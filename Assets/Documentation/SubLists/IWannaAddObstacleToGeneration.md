#### *Я хочу добавить обстакл в генерацию на плейтесте*

1. Открой файл [EObstacleType.cs](./../../Scripts/Game/Utils/Obstacles/EObstacleType.cs).

2. Добавь свой тип обстакла. Осторожно! Максимальное смещение: 31.

![SettingsAsset](./../HowToUseGenerationPictures/ObstacleTypes.png)

3. Открой файл [ObstaclesBase.asset](./../../Confg/Settings/ObstaclesBase.asset).

![ObstaclesAsset](./../HowToUseGenerationPictures/ObstaclesAsset.png)

4. Добавь свой тип обстаклов в словарь и прокинь их префабы. 
Если эти обстаклы должны встраиваться в чанк как стены, то поставь галочку
"Is Obstacle With Walls".

![AddObstacleToBase](./../HowToUseGenerationPictures/AddObstacleToBase.png)

5. Открой файл [ChunkGenerationSettings.asset](./../../Confg/Settings/ChunkGenerationSettings.asset).

![SettingsAsset](./../HowToUseGenerationPictures/SettingsAsset.png)

6. Добавь свой обстакл в словарь AvailableObstacles интересующего тебя чанка.

![AddObstacleToChunk](./../HowToUseGenerationPictures/AddObstacleToChunk.png)

7. Готово!