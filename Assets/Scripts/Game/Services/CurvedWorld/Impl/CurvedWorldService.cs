using System;
using AmazingAssets.CurvedWorld;
using Db.CurvedWorld;
using Db.CurvedWorld.Impl.Events;
using Db.GameLevelsSettings.Impls.Sequence;
using Game.Providers;
using PdUtils.RandomProvider;
using UnityEngine;

namespace Game.Services.CurvedWorld.Impl
{
    public class CurvedWorldService : ICurvedWorldService
    {
        private readonly CurvedWorldController _curvedWorldController;
        private readonly ICurvedWorldTypeParameters _curvedWorldTypeParameters;
        private readonly IRandomProvider _randomProvider;
        private readonly GameContext _game;
        private readonly CurvedWorldHelper _curvedWorldHelper;
        private ACurvedWorldTypeParametersBase _currentCurvedWorldTypeParametersBase;
        private static readonly int _curvedWorldBendSettings = Shader.PropertyToID("_CurvedWorldBendType");
        
        public CurvedWorldService(
            IGameFieldProvider gameFieldProvider,
            ICurvedWorldTypeParameters curvedWorldTypeParameters,
            IRandomProvider randomProvider,
            GameContext game
        )
        {
            _curvedWorldTypeParameters = curvedWorldTypeParameters;
            _randomProvider = randomProvider;
            _game = game;
            _curvedWorldController = gameFieldProvider.GameField.CurvedWorldController;
            _curvedWorldHelper = new CurvedWorldHelper(_curvedWorldController);
        }
        
        public int TrySetBendSettings(out bool isSuccessfully, SequenceParameters sequenceParameter)
        {
            var eventDurationInCountSequences = SetSendTypeWorldAndParameters(sequenceParameter);
            
            isSuccessfully = eventDurationInCountSequences != 0;
            return eventDurationInCountSequences;
        }
        
        public void ResetCurvedWorld()
        {
            switch (_currentCurvedWorldTypeParametersBase.BendType)
            {
                case BendType.CylindricalRolloff_X:
                case BendType.CylindricalRolloff_Z:
                    if (_currentCurvedWorldTypeParametersBase is CylindricalRolloffWorld cylindricalRolloffData)
                    {
                        _curvedWorldHelper.SetParamaterCurvatureWorld(
                            cylindricalRolloffData.BendCurvatureOffset, 
                            0f, 
                            cylindricalRolloffData.BendCurvatureSize, 
                            0f, 
                            cylindricalRolloffData.TimeToDisableCurvedWorld
                        );
                    }
                    break;
                case BendType.ClassicRunner_X_Positive:
                case BendType.ClassicRunner_Z_Positive:
                    if (_currentCurvedWorldTypeParametersBase is ClassicRunnerWorld classicRunnerZWorld)
                    {
                        _curvedWorldHelper.SetParamaterVerticalWorld(
                            classicRunnerZWorld.BendVerticalOffset, 
                            0f, 
                            classicRunnerZWorld.BendVerticalSize, 
                            0f, 
                            classicRunnerZWorld.TimeToDisableCurvedWorld
                        );
                        
                        _curvedWorldHelper.SetParamaterHorizontalWorld(
                            classicRunnerZWorld.BendHorizontalOffset, 
                            0f, 
                            classicRunnerZWorld.BendHorizontalSize, 
                            0f, 
                            classicRunnerZWorld.TimeToDisableCurvedWorld
                        );
                    }
                    break;
                case BendType.TwistedSpiral_X_Positive:
                case BendType.TwistedSpiral_Z_Positive:
                    if (_currentCurvedWorldTypeParametersBase is TwistedSpiralWorld twistedSpiralZWorld)
                    {
                        _curvedWorldHelper.SetParamaterHorizontalWorld(
                            twistedSpiralZWorld.BendHorizontalOffset,
                            0f, 
                            twistedSpiralZWorld.BendHorizontalSize,
                            0f, 
                            twistedSpiralZWorld.TimeToDisableCurvedWorld
                        );
                        
                        _curvedWorldHelper.SetParamaterVerticalWorld(
                            twistedSpiralZWorld.BendVerticalOffset, 
                            0f, 
                            twistedSpiralZWorld.BendVerticalSize, 
                            0f,
                            twistedSpiralZWorld.TimeToDisableCurvedWorld
                        );
                        
                        _curvedWorldHelper.SetParamaterCurvatureWorld(
                            twistedSpiralZWorld.BendCurvatureOffset, 
                            0f,
                            twistedSpiralZWorld.BendCurvatureSize, 
                            0f, 
                            twistedSpiralZWorld.TimeToDisableCurvedWorld
                        );
                    }
                    break;
            }
        }
        
        public void ResetCountCurvedWorldEventInLevel()
        {
            var player = _game.PlayerEntity;
            
            if (player.HasCountCurvedWorldEventInLevel)
                player.ReplaceCountCurvedWorldEventInLevel(0);
        }
        
        public void AddCountCurvedWorldEventInLevel(int count)
        {
            var player = _game.PlayerEntity;
            player.ReplaceCountCurvedWorldEventInLevel(player.CountCurvedWorldEventInLevel.Value + count);
        }
        
        private void ApplyCurvedWorld()
        {
            switch (_currentCurvedWorldTypeParametersBase.BendType)
            {
                case BendType.CylindricalRolloff_X:
                case BendType.CylindricalRolloff_Z:
                    if (_currentCurvedWorldTypeParametersBase is CylindricalRolloffWorld cylindricalRolloffData)
                    {
                        _curvedWorldHelper.SetParamaterCurvatureWorld(
                            0f,
                            cylindricalRolloffData.BendCurvatureOffset, 
                            0f,
                            cylindricalRolloffData.BendCurvatureSize,
                            cylindricalRolloffData.TimeInterpolationToCurveWorld
                        );
                    }
                    break;
                
                case BendType.ClassicRunner_X_Positive:
                case BendType.ClassicRunner_Z_Positive:
                    if (_currentCurvedWorldTypeParametersBase is ClassicRunnerWorld classicRunnerZWorld)
                    {
                        _curvedWorldHelper.SetParamaterVerticalWorld(
                            0f, 
                            classicRunnerZWorld.BendVerticalOffset,
                            0f,
                            classicRunnerZWorld.BendVerticalSize, 
                            classicRunnerZWorld.TimeInterpolationToCurveWorld
                        );
                        
                        _curvedWorldHelper.SetParamaterHorizontalWorld(
                            0f,
                            classicRunnerZWorld.BendHorizontalOffset,
                            0f,
                            classicRunnerZWorld.BendHorizontalSize,
                            classicRunnerZWorld.TimeInterpolationToCurveWorld
                         );
                    }
                    break;
                
                case BendType.TwistedSpiral_X_Positive:
                case BendType.TwistedSpiral_Z_Positive:
                    if (_currentCurvedWorldTypeParametersBase is TwistedSpiralWorld twistedSpiralZWorld)
                    {
                        _curvedWorldHelper.SetParamaterVerticalWorld(
                            0f, 
                            twistedSpiralZWorld.BendVerticalOffset, 
                            0f,
                            twistedSpiralZWorld.BendVerticalSize, 
                            twistedSpiralZWorld.TimeInterpolationToCurveWorld
                        );
                        
                        _curvedWorldHelper.SetParamaterHorizontalWorld(
                            0f, 
                            twistedSpiralZWorld.BendHorizontalOffset,
                            0f, 
                            twistedSpiralZWorld.BendHorizontalSize, 
                            twistedSpiralZWorld.TimeInterpolationToCurveWorld
                        );
                        
                        _curvedWorldHelper.SetParamaterCurvatureWorld(
                            0f, 
                            twistedSpiralZWorld.BendCurvatureOffset, 
                            0f, 
                            twistedSpiralZWorld.BendCurvatureSize, 
                            twistedSpiralZWorld.TimeInterpolationToCurveWorld
                        );
                    }
                    break;
            }
        }
        
        private int SetSendTypeWorldAndParameters(SequenceParameters sequenceParameter)
        {
            var curvedWorldTypeParameters = GetCurvedWorldTypeParameters(sequenceParameter);
            
            if (curvedWorldTypeParameters == null)
            {
                return 0;
            }
            
            _currentCurvedWorldTypeParametersBase = curvedWorldTypeParameters;
            _game.PlayerEntity.IsActiveCurvedWorldEvent = true;
            
            SetCurvedBendType(_currentCurvedWorldTypeParametersBase.BendType);
            
            ApplyCurvedWorld();
            
            return _currentCurvedWorldTypeParametersBase.EventDurationInCountSequences;
        }
        
        private ACurvedWorldTypeParametersBase GetCurvedWorldTypeParameters(SequenceParameters sequenceParameter)
        {
            SequenceParametersData sequenceParametersDataCurrentChunk = null;
            
            foreach (var sequenceParametersData in _curvedWorldTypeParameters.Parameters.Keys)
            {
                if (sequenceParametersData.SequenceParameters != sequenceParameter)
                    continue;
                    
                sequenceParametersDataCurrentChunk = sequenceParametersData;
                break;
            }
            
            if (sequenceParametersDataCurrentChunk == null)
                return null;
            
            if (!_curvedWorldTypeParameters.Parameters.TryGetValue(sequenceParametersDataCurrentChunk, out var curvedWorldEventGroup))
                throw new ArgumentNullException();
            
            var randomValue = _randomProvider.Range(0f, 1f);
            var cumulativeChance = 0f;
            
            foreach (var chanceEvent in curvedWorldEventGroup.ChanceTriggeringEvents)
            {
                cumulativeChance += chanceEvent.Value;
                
                if (!(randomValue <= cumulativeChance))
                    continue;
                
                return chanceEvent.Key;
            }
            
            return null;
        }
        
        private void SetCurvedBendType(BendType bendType)
        {
            _curvedWorldController.bendType = bendType;
            
            Shader.SetGlobalFloat(_curvedWorldBendSettings, (int)bendType);
        }
    }
}