# Технологии баз данных 
1.  Создать БД с полями Фамилия, Имя, Отчество, дата рождения. 
 Заполнить БД 1 млн рандомными записями: фио должны быть читаемыми, даты рождения в интервале 01.01.1995-31.12.2005
 Сделать несколько копий базы для выполнения последующих заданий (либо при выполнении заданий делать импорт данных с исходной таблицы).

2.  В созданную базу добавить поле "полных лет" и заполнить правильными значениями на текущую дату:
 а) используя update
 б) используя триггер
 в) используя хранимую процедуру
 В каждом из случаев применяем  код к исходной базе(отдельной копии) и замеряем время исполнения.
 В б) и в) допускается описание триггера и процедуры ДО добавления поля в структуру базы.

3.  В созданную базу добавить поле "количество однофамильцев" и заполнить правильными значениями:
 а) через Update без создания индексов
 б) через Update с предварительным созданием индекса по полю с фамилией
 в) используя хранимую функцию подсчета количества однофамильцев
 В каждом из случаев применяем код к исходной базе(отдельной копии) и замеряем время исполнения.
 В пункте б) время считаем отдельно на создание индекса и на выполнение Update, потом суммируем

Результаты ко всем трем пунктам и всем подпунктам предоставляем в виде:

1) исходные тексты
2) скрины с первых записей таблицы
3) скрины с временем работы
4) сводные сравнительные таблицы - для п.2 и п.3

Доп.задание. В каждом из 3а, 3б, 3в найти наиболее часто встречающуюся фамилию НЕ используя поле "количество однофамильцев".
 Вывести фамилию, количество и время поиска 
