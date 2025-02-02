# Task-8-WPF



## Creating db and inserting data

***Create db using sqlite:

Open Command Prompt, run
sqlite3 courseswpf.db
<expect: SQLite version 3.49.0 2025-01-28 12:50:17
Enter ".help" for usage hints.>

***To create tables:

CREATE TABLE courses (CourseId INTEGER PRIMARY KEY,
                                       Name TEXT, Description TEXT);


CREATE TABLE studentsGroups (Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                                    CourseId INTEGER NOT NULL,
                                                                     Name TEXT, TeacherId INTEGER,
                             FOREIGN KEY (CourseId) REFERENCES courses(CourseId),
                             FOREIGN KEY (TeacherId) REFERENCES teachers(TeacherId));


CREATE TABLE students (StudentId INTEGER PRIMARY KEY,
                                         GroupId INTEGER NOT NULL,
                                                         FirstName TEXT, LastName TEXT,
                       FOREIGN KEY (GroupId) REFERENCES studentsGroups (Id));


CREATE TABLE teachers (TeacherId INTEGER PRIMARY KEY,
                                         FirstName TEXT, LastName TEXT);

***To insert data:

INSERT INTO courses (Name, Description)
VALUES ('Business','Field of study that deals with the principles of business, management, and economics. It combines elements of accountancy, finance, marketing, organizational studies, human resource management, and operations.'),
       ('Sales' ,'Sales course covers a wide range of topics essential for developing effective selling skills. These include understanding the sales process, prospecting and lead generation, and building customer relationships.'),
       ('Marketing' ,'An examination of managerial decision-making and problem-solving using the marketing mix and the activities it entails such as selling, advertising, pricing, consumer behavior, marketing research and channels of distribution.'),
       ('Computer science' ,'The course is designed to teach the theoretical and practical aspects of computing, including programming, algorithms, data structures, software development, and computer systems.'),
       ('Hospitality' ,'Hospitality, culinary arts, catering, events, human resources, marketing, sales, and finance.'),
       ('Arts' ,'Combines a mixture of theoretical and practical course elements to nurture students in developing their own artistic work.');


INSERT INTO studentsGroups (Name, CourseId, TeacherId)
VALUES ('BR-01',1, 1) ,
       ('BR-02',1, 2) ,
       ('SR-01',2, 3) ,
       ('CS-01',4, 4) ,
       ('HP-01',5, 1) ,
       ('TA-01',6, 2) ,
       ('TA-02',6, 3) ,
       ('M-01',3, 4) ,
       ('M-02',3, 1);


INSERT INTO teachers (FirstName, LastName)
VALUES ('John', 'Smith'),
       ('Emily', 'Johnson'),
       ('Michael', 'Brown'),
       ('Sarah', 'Davis');


INSERT INTO students (GroupId, FirstName, LastName)
VALUES (1, 'Elijah', 'Hall') ,
       (1, 'Ethan', 'Anderson') ,
       (1, 'Olivia', 'Brown') ,
       (1, 'Liam', 'Campbell') ,
       (1, 'Ava', 'Carter') ,
       (1, 'Noah', 'Clark') ,
       (1, 'Sophia', 'Davis') ,
       (1, 'Mason', 'Edwards') ,
       (1, 'Isabella', 'Evans') ,
       (1, 'Lucas', 'Foster') ,
       (2, 'Mia', 'Garcia') ,
       (2, 'Benjamin', 'Gray') ,
       (2, 'Amelia', 'Green') ,
       (2, 'Elijah', 'Hall') ,
       (2, 'Charlotte', 'Harris') ,
       (2, 'Alexander', 'Hill') ,
       (2, 'Harper', 'Hughes') ,
       (2, 'James', 'Jackson') ,
       (2, 'Evelyn', 'Johnson') ,
       (2, 'William', 'Jones') ,
       (2, 'Abigail', 'Kelly') ,
       (2, 'Logan', 'Lewis') ,
       (1, 'Emily', 'Lopez') ,
       (1, 'Jacob', 'Martin') ,
       (3, 'Lily', 'Martinez') ,
       (3, 'Michael', 'Miller') ,
       (3, 'Grace', 'Mitchell') ,
       (3, 'Daniel', 'Moore') ,
       (3, 'Chloe', 'Nelson') ,
       (3, 'Henry', 'Parker') ,
       (6, 'Zoe', 'Perez') ,
       (6, 'Matthew', 'Phillips') ,
       (6, 'Ella', 'Powell') ,
       (6, 'Jackson', 'Roberts') ,
       (6, 'Scarlett', 'Robinson') ,
       (6, 'Aiden', 'Rodriguez') ,
       (6, 'Aria', 'Scott') ,
       (6, 'Samuel', 'Simmons') ,
       (5, 'Ella', 'Stewart') ,
       (5, 'Luke', 'Taylor') ,
       (5, 'Hannah', 'Thompson') ,
       (5, 'Jack', 'Walker') ,
       (5, 'Madison', 'White') ,
       (5, 'Owen', 'Williams') ,
       (5, 'Nora', 'Wilson') ,
       (5, 'Caleb', 'Young') ,
       (4, 'Emma', 'Smith') ,
       (4, 'Liam', 'Johnson') ,
       (5, 'Noah', 'Williams') ,
       (5, 'Ava', 'Jones') ,
       (8, 'Sophia', 'Brown') ,
       (8, 'Mason', 'Davis') ,
       (8, 'Isabella', 'Miller') ,
       (5, 'Ethan', 'Wilson') ,
       (5, 'Olivia', 'Moore') ,
       (5, 'Lucas', 'Taylor') ,
       (5, 'Amelia', 'Anderson') ,
       (4, 'Emma', 'Smith') ,
       (4, 'Henry', 'Johnson') ,
       (5, 'Mia', 'Williams') ,
       (7, 'James', 'Jones') ,
       (7, 'Charlotte', 'Brown') ,
       (7, 'Alexander', 'Davis') ,
       (7, 'Grace', 'Miller') ,
       (7, 'Benjamin', 'Wilson') ,
       (7, 'Harper', 'Moore') ,
       (7, 'Jack', 'Taylor') ,
       (7, 'Ella', 'Brown') ,
       (7, 'Samuel', 'Davis') ,
       (7, 'Scarlett', 'Miller') ,
       (7, 'Logan', 'Wilson') ,
       (7, 'Abigail', 'Moore') ,
       (7, 'Abigail', 'Young');
	   
***To verify the insertion:
SELECT *
FROM courses;

SELECT *
FROM studentsGroups;

SELECT *
FROM teachers;

SELECT *
FROM students;