using System.Globalization;
using SPT_Management_System_Console_App;
using SPT_Management_System_Console_App.Models_Classes;
using var db = new AppDbContext();
bool dbExists = await db.Database.CanConnectAsync(); 
if (dbExists = true) 
{
    Console.BackgroundColor = ConsoleColor.Green;
    Console.WriteLine("Database Exists");
    Console.Beep();
    Console.BackgroundColor = ConsoleColor.Black;
}
else {
    db.Database.EnsureCreated(); //if you dont add this it wont create the table in the database
    //this makes it create the "student table database by force"
}


//Method calling area and "Official" starting point of code
Menu();





//Methods Used or written to be used at some point
void Menu()
{
    Console.WriteLine("Welcome to the Student Productivity Toolkit");
    Console.WriteLine("Press Corresponding key to select from the following options");
    Console.WriteLine("[A] Profile Creation");
    Console.WriteLine("[B] Course Management");
    Console.WriteLine("[C] Results Management");
    Console.WriteLine("[D] Exit");
    ConsoleKeyInfo pressedKey = Console.ReadKey();
    Console.Clear();
    switch (pressedKey.Key)
    {
        case ConsoleKey.A:
            Console.WriteLine("[Profile Creation]");
            var newStudent = ProfileCreation();
            db.StudentTable.Add(newStudent);
            db.SaveChanges();
            Console.WriteLine("Profile Created");  
            break;

        case ConsoleKey.B:
            Console.WriteLine("[Course Management]");
            CourseManagement();
            break;
        case ConsoleKey.C:
            ResultManagement();
            break;
        case ConsoleKey.D:
            Environment.Exit(0);
            break;

        default:
            Console.WriteLine("Invalid Key pressed");
            break;
    }

}
List<Object> Login()
{
    bool slValid = false;
    List<object> validLogin = new List<object>();
    while (slValid == false)
    {
        Console.Write("Enter Your Student Login: ");
        string _tempSL = Convert.ToString(Console.ReadLine());//_tempSL-------> _temp student login
        if (_tempSL.Length != 6)
        {
            Console.WriteLine("Invalid Student Login, Please Try again");
            continue;
        }
        var student = db.StudentTable.FirstOrDefault(s => s.studentLogin == _tempSL);
        if (student != null)
        {
            slValid = true;
            Console.WriteLine("Login Succesful");
        }
        else
        {
            Console.WriteLine("Invalid Student Login, Please Try again");
            continue;
        }
        validLogin.Add(_tempSL);
        validLogin.Add(student);
    }
    return validLogin;
}
void/*for now , later it returns a string*/ RecoverStudentLogin()
{
    Console.WriteLine("Enter Your First name");
    Console.WriteLine("Enter Your Last name");
    Console.WriteLine("Enter Your Department");



    //search the database for the entries find more things to narrow down sha 
    // produce the student login string from the database
}
Student_Model ProfileCreation()
{
    Student_Model student = new Student_Model();
    Console.Write("Enter your FirstName: ");
    student.firstName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Console.ReadLine());
    Console.Write("Enter your LastName: ");
    student.lastName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Console.ReadLine());
    Console.Write("Enter your Course of study [Department]: ");
    student.department = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Console.ReadLine());
    Console.Write("Enter your Level(100,200,300,400,500): ");
   
    var level = new Dictionary<int, string> 
    { {100,"First Year" }, {200 ,"Second Year"}, {300,"Third Year"},{ 400, "Fourth Year"}, {500,"Fifth Year" },{009,"Unspecified"} };
    int input = int.Parse(Console.ReadLine());
    if (level.ContainsKey(input))
    {
        student.level = level[input];
        student._numLevel = (uint)input;
    }
    else 
    {
        Console.WriteLine("You have entered an Invalid Level\n Level is Automatically set to Unspecified");
        input = 009;
        student.level = level[input];
        student._numLevel = (uint)input;
    }
    string studentLogin = student.firstName.Substring(0,2).ToLower() + student.lastName.Substring(0, 2).ToLower() + student.uniqueUserId.Substring(4,2) ;
    student.studentLogin = studentLogin;
    Console.WriteLine($"[{studentLogin}] is your login string\nKEEP IT SAFE");
    return student;
}
void CourseManagement()
{
    var loginCred = Login();
    while (true)
    {
        Console.WriteLine("Press Corresponding key to select from the following options");
        Console.WriteLine("[A] Add Course");
        Console.WriteLine("[B] Remove Course");
        Console.WriteLine("[C] View Courses");// maybe add an edit courses..?
        Console.WriteLine("[D] Exit");
        ConsoleKeyInfo pressedKey = Console.ReadKey();
        Console.Clear();
        switch (pressedKey.Key)
        {
            case ConsoleKey.A:
                Console.WriteLine("[Adding Courses]");
                AddCourses(loginCred);
                break;
            case ConsoleKey.B:
                Console.WriteLine("[Removing Courses]");
                RemoveCourse(loginCred);
                break;
            case ConsoleKey.C:
                Console.WriteLine("[Viewing Courses]");
                ViewCourse(loginCred);
                break;
            case ConsoleKey.D:
                Console.WriteLine("[Exiting...]");
                return;



            default:
                Console.WriteLine("Invalid Option Selected");
                continue;
        }
    }
}
void AddCourses(List<Object> _loginCred)
{
    string _tempSL = _loginCred[0].ToString(); // its the same name as the one inside login coz i was lazy to change it
    var student = (Student_Model)_loginCred[1];// same as the previous comment
   
    var regCourses = new List<Course_Model>();
    Console.Write("You will be prompted to enter some information about your course\nEnter 'done' as course code to finish to finish\n");
    int i = 1;
    while (true)
    {

        Console.WriteLine($"-------------------------------Course {i}-----------------------------");
        
        Console.Write("Enter The Course Code: ");
        string cCode = Convert.ToString(Console.ReadLine()).ToUpper();
        if (cCode == "DONE")
        {
            Console.WriteLine("Course(s) Saved");
            break;
        }
        Console.Write("Do you wish to enter a course title?[Y/N]: ");
        ConsoleKeyInfo key = Console.ReadKey();
        string cTitle = null;
        switch (key.Key)
        {
            case ConsoleKey.Y:
                Console.Write("\nEnter The Course Title: ");
                cTitle = Convert.ToString(Console.ReadLine());
                i++;
                break;
            case ConsoleKey.N:
                cTitle = "NaN";
                Console.WriteLine();
                i++;
                break;
            default:
                Console.WriteLine("press the correct key");
                continue;
        }
        Console.Write("Enter The Course unit: ");
        if (!uint.TryParse(Console.ReadLine(), out uint cUnit))
        {
            Console.WriteLine("Invalid Unit Please enter a number");
            continue;
        }

        var course = new Course_Model()
        {
            courseCode = cCode,
            courseUnit = cUnit,
            courseName = cTitle,
            _CuniqueUserId = db.StudentTable.Where(s => s.studentLogin == _tempSL).Select(s => s.studentLogin).FirstOrDefault(),
            Student = student
        };
        db.CourseTable.AddRange(course);
        db.SaveChanges();
    }
}
List<Course_Model> ViewCourse(List<Object> _loginCred)
{
    string _tempSL = _loginCred[0].ToString(); // its the same name as the one inside login coz i was lazy to change it
    var student = (Student_Model)_loginCred[1];
    var CuniqueUserId = db.StudentTable.Where(s => s.studentLogin == _tempSL).Select(s => s.uniqueUserId).FirstOrDefault();
    var viewCourse = new Course_Model();


    List<Course_Model> viewCourseList = new List<Course_Model>();

    List<string> CourseCodes = db.CourseTable.Where(c => c._CuniqueUserId == CuniqueUserId).Select(s => s.courseCode).ToList();
    List<string> CourseNames = db.CourseTable.Where(c => c._CuniqueUserId == CuniqueUserId).Select(s => s.courseName).ToList();
    List<uint> CourseUnit = db.CourseTable.Where(c => c._CuniqueUserId == CuniqueUserId).Select(s => s.courseUnit).ToList();


    Console.WriteLine($"[S/N] {"Course Codes",-15} {"Course Unit",-10}  {"Course Description",5} ");
    for (int i = 0; i < CourseCodes.Count; i++)
    {
        Console.WriteLine($"[{i + 1}] {CourseCodes[i],-20} {CourseUnit[i],-10}  {CourseNames[i],5} ");
        viewCourse.courseCode = CourseCodes[i];
        viewCourse.courseUnit = CourseUnit[i];
        viewCourse.courseName = CourseNames[i];
        viewCourse._CuniqueUserId = CuniqueUserId;
        viewCourseList.Add(viewCourse);
    }
    Console.WriteLine("---------------------------------------------------------------------------------------------------");
    return viewCourseList ;
}
void RemoveCourse(List<Object> _loginCred)
{
    string _tempSL = _loginCred[0].ToString(); // its the same name as the one inside login coz i was lazy to change it
    var student = (Student_Model)_loginCred[1];
    var CuniqueUserId = db.StudentTable.Where(s => s.studentLogin == _tempSL).Select(s => s.uniqueUserId).FirstOrDefault();
    ViewCourse(_loginCred);
    Console.WriteLine("Enter \"done\" to exit");

    while (true)
    {
        Console.Write("Enter the Course Code to be deleted: ");
        string userDelCode;
        userDelCode = Console.ReadLine().ToUpper();
        if (userDelCode != "DONE")
        {
            if (db.CourseTable.Any(c => c.courseCode == userDelCode))
            {
                Course_Model toRemove = db.CourseTable.Where(c => c.courseCode == userDelCode).FirstOrDefault();// not workin
                db.CourseTable.Remove(toRemove);
                db.SaveChanges();
                Console.WriteLine("Course Removed Succesfully");
                
            }
            else
            {
                Console.WriteLine("This Course Doesnt Exist \t Try Again");
                continue;
            }
        }
        else { break; }
        ViewCourse(_loginCred);
    }
}
void ResultManagement()
{
    var loginCred = Login();
    Console.WriteLine("Press Corresponding key to select from the following options");
    Console.WriteLine("[A] Grades Upload");
    Console.WriteLine("[B] GPA Calculator");
    Console.WriteLine("[C] \"placeholder\"");// maybe add an edit courses..?
    Console.WriteLine("[D] Exit");
    ConsoleKeyInfo pressedKey = Console.ReadKey();
    Console.Clear();
    switch (pressedKey.Key)
    {
        case ConsoleKey.A:
            Console.WriteLine("Results Upload");
            GradeUpload(loginCred);
            break;
        case ConsoleKey.B:
            Console.WriteLine("GPA calc");
            break;
        case ConsoleKey.C:

            break;
        case ConsoleKey.D:

            return;



        default:

            break;

    }
}
void GradeUpload(List<Object> _loginCred)
{
    string _tempSL = _loginCred[0].ToString(); // its the same name as the one inside login coz i was lazy to change it
    var student = (Student_Model)_loginCred[1];
    var CuniqueUserId = db.StudentTable.Where(s => s.studentLogin == _tempSL).Select(s => s.uniqueUserId).FirstOrDefault();
    ViewCourse(_loginCred);
    

    List<string> CourseCodes = db.CourseTable.Where(c => c._CuniqueUserId == CuniqueUserId).Select(s => s.courseCode).ToList();
    List<uint> CourseUnit = db.CourseTable.Where(c => c._CuniqueUserId == CuniqueUserId).Select(s => s.courseUnit).ToList();
    
    int i = 0;
    while (i < CourseCodes.Count )
    {
        Console.Write($"Enter the Grade [A,B,C,D,E,F] for {CourseCodes[i]}:  ");
        char gradeChar = 'M';
        ConsoleKeyInfo pressedKey = Console.ReadKey();
        Console.Clear();
        switch (pressedKey.Key)
        {
            case ConsoleKey.A:
                gradeChar = pressedKey.KeyChar;
                Console.WriteLine($"{CourseCodes[i]}: {gradeChar}");
                i++;
                break;
            case ConsoleKey.B:
                gradeChar = pressedKey.KeyChar;
                Console.WriteLine($"{CourseCodes[i]}: {gradeChar}");
                i++;
                break;
            case ConsoleKey.C:
                gradeChar = pressedKey.KeyChar;
                Console.WriteLine($"{CourseCodes[i]}: {gradeChar}");
                i++;
                break;
            case ConsoleKey.D:
                gradeChar = pressedKey.KeyChar;
                Console.WriteLine($"{CourseCodes[i]}: {gradeChar}");
                i++;
                break;
            case ConsoleKey.E:
                gradeChar = pressedKey.KeyChar;
                Console.WriteLine($"{CourseCodes[i]}: {gradeChar}");
                i++;
                break;
            case ConsoleKey.F:
                gradeChar = pressedKey.KeyChar;
                Console.WriteLine($"{CourseCodes[i]}: {gradeChar}");
                i++;
                break;

            default:
                Console.WriteLine("Enter the Correct Grade");
                continue;
        }
        var grade = new Grades_Model()
        {
            _GuniqueUserId = db.StudentTable.Where(s => s.studentLogin == _tempSL).Select(s => s.studentLogin).FirstOrDefault(),
            courseCode = CourseCodes[i],
            courseUnit = CourseUnit[i],
            grade = gradeChar,
            Student = student,
        };
        db.GradesTable.AddRange(grade);
        db.SaveChanges();
    }
}
void GPACalculator(List<Object> _loginCred)
{
    string _tempSL = _loginCred[0].ToString(); // its the same name as the one inside login coz i was lazy to change it
    var student = (Student_Model)_loginCred[1];
    var CuniqueUserId = db.StudentTable.Where(s => s.studentLogin == _tempSL).Select(s => s.uniqueUserId).FirstOrDefault();
    ViewCourse(_loginCred);

    List<string> CourseCodes = db.CourseTable.Where(c => c._CuniqueUserId == CuniqueUserId).Select(s => s.courseCode).ToList();
    List<string> CourseNames = db.CourseTable.Where(c => c._CuniqueUserId == CuniqueUserId).Select(s => s.courseName).ToList();
    List<uint> CourseUnit = db.CourseTable.Where(c => c._CuniqueUserId == CuniqueUserId).Select(s => s.courseUnit).ToList();

    Dictionary<Char, int> gradePoints = new Dictionary<char, int>()
    {
        {'A',5 },
        {'B',4 },
        {'C',3 },
        {'D',2 },
        {'E',1 },
        {'F',0 }
    };

}

//ideas to explore: 
// store  student login pair but encrypted student model
