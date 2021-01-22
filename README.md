# Asteroids-1979
 Demo for primary Unity skills

 Unity version 2020.1.0a18
 Need installed Playmaker + iTween


 Это демо создано для демонстрации основных умений и понимания работы Unity и C#.

 		Содержание
 1. Описание
 2. Сцены
 	2.1 Сцена MainMenu
 	2.2 Сцена MainGameScene
 		2.2.1 Камеры
 		2.2.2 Canvas
 		2.2.3 Менеджеры
 		2.2.4 Player
 		2.2.5 UFO
 		2.2.6 Префабы астероидов
 		2.2.7 Префаб снаряда
 		2.2.8 Префаб взрыва
 		2.2.9 Интерфейс
3. Заключение





 1. Описание

 Проект представляет из себя ремейк платформера Asteroids.
 Суть игры: игрок управляет космическим кораблём, который находится в поле астероидов.
 Астероиды можно уничтожать выстрелами пушки корабля. 
 Астероиды могут быть трёх размеров, при попадании раскалываются на более мелкие.
 Периодически прилетает НЛО.



 2. Сцены
 	Игра разбита на 2 сцены, MainMenu и MainGameScene.

Общие элементы сцен:
При загрузке сцен используется анимация(первая часть запускается при загрузке сцены), сглаживающая резкую загрузку сцены
посредством изменения альфа-канала изображения Canvas под названием Crossfade.
Canvas упакован в объект LevelLoader со скриптом STDLevelLoader.
Скрипт-синглтон имеет единственный метод LoadNextLevel()
запускающий корутин с анимацией (вторая половина) и загрузающий сцену со следующим индексом. 


	2.1 	Сцена MainMenu:

Представляет из себя Canvas с картинкой-бекграундом, на котором размещены кнопки интерфейса пользователя.

В верхнем правом углу расположена панель (под углом к Canvas) со стандартными кнопками UI/Button,
работа которых настроена через FSM Playmaker

Кнопка Play вызывает метод LoadNextLevel(), что загружает следующую сцену.
Кнопка About запускает анимацию показа текста a la Star Wars, идущего вверх под углом к плоскости экрана
Кнопка Exit закрывает приложение


В нижнем левом углу расположена шестерня показа/скрытия регуляторов настройки звука.
При нажатии срабатывает анимация с выезжающим слайдером регулятора звука.
Регулятор передаёт своё значение скрипту MixerController, через единственный метод SetSoundLevel(float)


	2.2	Сцена MainGameScene, список объектов

	2.2.1 Камеры - основная (ортографик) и вспомогательная (ортографик) для мини-радара.
Вспомогательная передаёт изображение в текстуру MiniMap, которая используется в объекте Radar  составе Canvas,
радар отображается в нижнем левом углу экрана

------------------------------------------------------

	2.2.2 Canvas/Panel GameOver

При гибели корабля активируется анимация выезда панели с надписью GameOver и кнопками To Main Menu и Exit to Desktop
К панели приаттачен скрипт GameOverPanel, 
кнопка To Main Menu вызывает метод ToMainMenu(), загружающий сцену с индексом 0
кнопка Exit вызывает метод ExitToDesctop(), закрывающий приложение

------------------------------------------------------
	2.2.3 Менеджеры: дженерик-синглтоны, наследуются от класса MonoSingleton

	GameManager 

В инспекторе содержит интерактивные поля для упрощения работы гейм-дизайнера, 
 - поле префаба корабля игрока;
 - поля настройки буферной зоны за пределами видимого экрана, в которой респавнятся астероиды
 - интервал респавна астероидов
 - поля для индикаторов текущих очков и High Scores
 - поле для префаба НЛО
 - поле максимального здоровья НЛО

GameManager при старте считывает из PlayerPrefs значение High Scores для отображения вверху экрана,
устанавливает случайный интервал между респавнами НЛО,
инстанцирует публичные свойства  координат центра экрана и размеров экрана,
задаёт буферную зону для запуска астероидов за пределами экрана,
запускает первые 10 астероидов

В методе Update() по таймеру запускает новые астероиды,
производит поворот Скайбокса для создания ощущения движения,
выводит на экран количество заработанных очков,
по таймеру запускает НЛО

В методе LateUpdate() производится контроль за выход игроком за пределы основной камеры, 
и переброска его в противоположную точку методом ChangePosition(Vector3)

Вспомогательные методы:
LaunchAsteroid() - запрашивает из пула астероид, выставляет ему максимальный размер и ХП,
помещает астероид на случайную сторону буферной зоны запуска, и придаёт ему импульс в сторону
случайной точки противоположной стороны экрана

TripleSpawn(Transform, int) - получает координаты умершего астероида, запрашивает анимацию взрыва из пула взрывов, проверяет, нужно ли умерший астероид расколоть на более мелкие,
помещает мелкие астероиды вокруг точки гибели родительского и раскидывает их взрывом

SummonEnemy() - выставляет НЛО максимальное ХП и помещает его в случайную точку за пределами экрана

SaveHiScore() - записывает в PlayerPrefs рекорд, если текущие очки больше рекорда 

	

	AsteroidPoolManager

В инспекторе содержит поле контейнера с астероидами,
префабы разных визуализаций астероидов,
поле звука удара астероидов

При старте генерирует 50 астероидов в пул объектов

Метод RequestAsteroid(int) - возвращает астероид заданного размера, 
реализует паттерн ObjectPool

Метод CreateAsteroid() - метод паттерна ObjectPool, создающий объект при старте или при отсутствии в пуле свободного объекта


	BulletPoolManager 

В инспекторе содержит поле контейнера снарядов,
поле префаба снаряда.

Реализован аналогично по паттерну ObjectPool


	ExplosionPoolManager

В инспекторе содержит поле контейнера взрывов,
поле префаба взрывов,
поле звука взрыва

Реализован аналогично по паттерну ObjectPool



-------------------------------------------------
	2.2.4 Player

Инстанцированный префаб корабля игрока.
В составе префаба скрипт PlayerController, осуществляющий управление и взаимодействие с другими объектами
Для удобства гейм-дизайнера инспектор скрипта содержит поля:
 - скорости поворота корабля
 - скорости движения корабля
 - поле задержки между выстрелами
 - поля частиц для визуализации выхлопа дюз
 - поле точки появления снаряда (места выстрела)
 - поле слайдера индикатора ХП игрока. Слайдер выводится в верхнем левом углу экрана
 - AudioSource 
 - аудиоклип звука выстрела
 - аудиоклип звука опасного уровня ХП
 - поле аниматора панели Game Over

PlayerController реализует интерфейс IDamageable (свойства Power, Health и метод Damage(int))

При старте инициализируется поле Health, задавая максимальное ХП корабля
Метод Update реализует управление кораблём, поворот влево-вправо, тягу основного движка и анимацию двигателей,
а так же стрельбу

Метод OnTriggerEnter(Collider) отслеживает касание с другими коллайдерами и вызывает метод Damage(int) 
у объекта с которым соприкоснулся

Damage (int) уменьшает ХП корабля на заданное значение, проверяет оставшийся ХП, проигрывает в случае необходимости
предупреждающий звук и вызывает метод KillShip() при падении ХП до нуля

KillShip() просит GameManager записать очки в рекорд, включает анимацию панели GameOver и выключает объект Player

-----------------------------------------------------------
	2.2.5 UFO

Префаб НЛО, со скриптами EnemyBehavior и EnemyController

	EnemyBehavior реализует интерфейс IDamageable, 
метод OnTriggerEnter отслеживает касание с другими коллайдерами и вызывает метод Damage(int) 
у объекта с которым соприкоснулся

Damage (int) уменьшает ХП НЛО на заданное значение, проверяет оставшийся ХП, и вызывает метод DestroyEnemy() при падении ХП до нуля

DestroyEnemy() вызывает из пула 10 взрывов по рандомной площади и выключает НЛО


EnemyController имеет поля для дизайнера:
 - поле объекта игрока, за которым необходимо охотиться
 - поле скорости движения
 - поля размера осей эллипса вокруг НЛО, на линии которого в случайном направлении спавнятся выстрелы
 - поле задержки выстрела
 - поле звука полёта НЛО

метод Update задаёт движение к кораблю игрока и стреляет по таймеру, получая данные от RNDDotOnEllipse()

метод RNDDotOnEllipse() возвращает кортеж из двух Vector3, точка на линии эллипса стрельбы и направление от центра эллипса

---------------------------------------------------------------

	2.2.6 Префабы астероидов

Три префаба разной визуализации, скачаны из Unity Asset Store
содержат скрипт RandomRotator и AsteroidBehavior

RandomRotator имеет поля минимума и максимума скорости вращения, при старте задаёт вращение астероида в случайную сторону и со случайной скоростью в заданном диапазоне

AsteroidBehavior реализует интерфейс IDamageable
имеет свойство Size, от которого зависит ХП и размер астероида

Update отслеживает расстояние до центра экрана и выключает объект при удалении за границы видимости, возвращая его в пул астероидов

OnTriggerEnter(Collider) даёт столкнувшемуся объекту повреждения в зависимости от размера

Damage(int) уменьшает ХП астероида и вызывает метод DestroyAsteroid() при падении ХП до нуля

DestroyAsteroid() вызывает проигрывание звука методом WhenDestroyAsteroid(), выключает астероид и сообщает GameManager о гибели астероида, вызывая три новых астероида меньшего размера на своё место

--------------------------------------------------------------

	2.2.7 Префаб снаряда

Содержит скрипт BulletBehavior, реализующий интерфейс IDamageable

Имеет свойство Scoreable, через которое задаётся, нужно ли зачислять попадание снаряда в счёт очков игрока.
Player при выстреле выставляет свойству true, UFO ставит false

Update двигает снаряд вперёд

OnTriggerEnter(Collider) вызывает метод Damade(int) у объекта, в который произошло попадание
снаряд выключается, если Scoreable = true, прибавляем очко игроку

OnBecameInvisible() выключает снаряд при выходе за фруструм камер, возвращая снаряд в пул

-----------------------------------------------------------------

	2.2.8 Префаб взрыва

Партикл из Unity Asset Store, добавлен скрипт ExplosionBehavior, 
Update проверяет, если проигрывание партикла закончилось, объект выключается.
При вызове из пула объект активируется запрашивающим скриптом, запуская анимацию

-------------------------------------------------------------------

	2.2.9 Интерфейс

Интерфейс IDamageable содержит свойства Power, Health и метод Damage(int)
свойство Power предназначено для установки ущерба, который наносится объектом
свойство Health предназначено для установки максимального ХП 
метод Damage(int) принимает количество повреждений от объекта, с которым произошло столкновение

--------------------------------------------------------------------

	3. Заключение

Проект предназначен исключительно для демонстрации знакомства с разными аспектами редактора Unity,
знакомства с простейшими паттернами, интерфейсами, и некоторых плюшек C# типа кортежей,
а так же демонстрацией того, что автор может как в код, так и в визуальное программирование типа Playmaker.

Поэтому реализация проекта выглядит "несколько странно"

P.S. большой вес ассетов выглядит удручающе, но задачи оптимизации не стояло, ассеты брались как есть



