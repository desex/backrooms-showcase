#### *Я хочу добавить чанк в генерацию на плейтесте*

1. Открой файл [EChunkType.cs](./../../Scripts/Game/Utils/Chunks/EChunkType.cs).

2. Добавь свой тип чанка. Осторожно! Максимальное смещение: 31.

![SettingsAsset](./../HowToUseGenerationPictures/ChunkTypes.png)

3. Открой файл [ChunkGenerationSettings.cs](./../../Scripts/Db/Chunks/Impls/ChunkGenerationSettings.cs)

4. Добавь новый ChunkTypeInfo для своего чанка.

![GenerationAddChunk](./../HowToUseGenerationPictures/GenerationAddChunk.png)

5. Добавь чанк в валидацию.

![AddChunkToValidation](./../HowToUseGenerationPictures/AddChunkToValidation.png)

6. Открой файл [ChunkGenerationSettings.asset](./../../Confg/Settings/ChunkGenerationSettings.asset).

![SettingsAsset](./../HowToUseGenerationPictures/SettingsAsset.png)

7. Прокинь префаб своего чанка

![](./../HowToUseGenerationPictures/AddChunkPrefab.png)

8. В настройках дистанс групп отставь одну группу и удостоверься, что там есть твой чанк.

![DistanceGroupForNewChunk](./../HowToUseGenerationPictures/DistanceGroupForNewChunk.png)

9. Добавь настройки переходов из твоего чанка и в твой чанк.

![AddChunkSwitches](./../HowToUseGenerationPictures/AddChunkSwtches.png)

10. Готово!