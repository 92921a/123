List<School> people = new List<School>();

while (true)
{
    Console.WriteLine("输入0或1，0创建教师，1创建学生，输入end停止");
    string? a = Console.ReadLine();
    if (a?.ToLower() == "end")
        break;
    try
    {
    Judge(a);
    PrintResult(people);
    }
    catch (Exception ex)
    {
        System.Console.WriteLine($"错误:{ex.Message}");
    }
}

void Judge(string? a)
{
    if (string.IsNullOrWhiteSpace(a))
    {
        Console.WriteLine("输入不为空");
    }
    switch (a)
    {
        case "0":
            Add(people, member.Teacher);
            break;
        case "1":
            Add(people, member.Student);
            break;
        default:
            Console.WriteLine("输入错误");
            break;
    }
}

static void Add(List<School> people, member member)
{
    System.Console.WriteLine($"输入{member}姓名：");
    var name = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(name))
    {
        throw new ArgumentException("姓名不能为空");
    }
    switch (member)
    {
        case member.Teacher:
            people.Add(new Teacher(name));
            break;
        case member.Student:
            System.Console.WriteLine("输入课程");
            var course = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(course))
            {
                throw new ArgumentNullException("课程不能为空");
            }
            people.Add(new Student(name, course));
            break;
    }
}

static void PrintResult(List<School> people)
{
    if (people == null || !people.Any())
    {
        System.Console.WriteLine("没有内容");
        return;
    }

    var sortedPeople = people.OrderBy(p => p.Type()).ToList();
    Console.WriteLine("所有输入内容：");

    int count = 0;

    foreach (var final in sortedPeople)
    {
        Console.WriteLine($"序列: {count++}, 身份: {final.Type()}, 姓名: {final.Name}");

        switch (final)
        {
            case Teacher teacher:
                teacher.Teach();
                teacher.GoSchool();
                break;

            case Student student:
                student.Learn();
                student.GoSchool();
                System.Console.WriteLine($"课程：{student.Course}");
                break;
        }
    }
}

public enum member
{
    Teacher,
    Student
}
public abstract class School
{
    public string Name { get; set; }

    public School(string name)
    {
        Name = name;
    }

    public abstract void GoSchool();
    public abstract string Type();
}

public class Teacher : School
{
    public Teacher(string name) : base(name) { }

    public override void GoSchool()
    {
        Console.WriteLine("调用老师goschool方法");
    }

    public void Teach()
    {
        Console.WriteLine("调用teach方法");
    }

    public override string Type() => "教师";
}

public class Student : School
{
    public string Course { get; set; }
    public Student(string name, string course) : base(name)
    {
        Course = course;
    }

    public override void GoSchool()
    {
        Console.WriteLine("调用学生goschool方法");
    }

    public void Learn()
    {
        Console.WriteLine($"调用learn方法");
    }

    public override string Type() => "学生";
}


