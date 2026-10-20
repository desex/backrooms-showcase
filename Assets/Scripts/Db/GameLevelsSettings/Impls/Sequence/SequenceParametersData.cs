using System;
using System.Collections.Generic;
using System.Linq;
#if UNITY_EDITOR
using Db.Utils;
#endif
using Sirenix.OdinInspector;
using UnityEngine;

namespace Db.GameLevelsSettings.Impls.Sequence
{
    [Serializable]
    [HideReferenceObjectPicker]
    public class SequenceParametersData
    {
#if UNITY_EDITOR
        [ValueDropdown(nameof(GetSequenceParametersNames))]
        [OnValueChanged(nameof(SetParameter))]
        [HideLabel]
        [SerializeField]
        private string _parameterName;
#endif

        [field: ShowIf(nameof(IsShowParameter))]
        [field: OnValueChanged(nameof(ChangeSequenceParameters))]
        [field: Required]
        [field: SerializeField]
        public SequenceParameters SequenceParameters { get; private set; }

        public SequenceParametersData Init(SequenceParameters parameters)
        {
            SequenceParameters = parameters;
#if UNITY_EDITOR
            _parameterName = parameters.name;
#endif

            return this;
        }
        
        private void ChangeSequenceParameters(SequenceParameters parameters)
        {
            if (parameters != null)
                return;
#if UNITY_EDITOR
            _parameterName = "null";
#endif
        }
        
        private bool IsShowParameter()
        {
            return SequenceParameters == null;
        }

#if UNITY_EDITOR
        [OnInspectorInit]
        private void Validate() =>
            ChangeSequenceParameters(SequenceParameters);
        
        private List<string> GetSequenceParametersNames()
        {
            return ReflectionUtils
                  .GetAll<SequenceParameters>()
                  .Select(parameter => parameter.name)
                  .ToList();
        }

        private void SetParameter(string parameterName)
        {
            var sequenceParameters =
                ReflectionUtils
                   .GetAll<SequenceParameters>()
                   .ToList()
                   .First(parameter => parameter.name == parameterName);

            SequenceParameters = sequenceParameters;
        }
#endif
    }
}