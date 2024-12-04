List<School> people = new List<School>();

while (true)
{
    Console.WriteLine("输入0或1，0创建教师，1创建学生；输入2删除，输入3修改信息，输入print打印结果，输入end停止");
    string? a = Console.ReadLine();

    try
    {
        Judge(a, people);
    }
    catch (InputException ex)
    {
        System.Console.WriteLine($"错误:{ex.Message}");
    }
}

void Judge(string? a, List<School> people)
{
    InputCheck(a);

    switch (a)
    {
        case "0":
            Add(people, member.Teacher);
            break;
        case "1":
            Add(people, member.Student);
            break;
        case "2":
            Remove(people);
            break;
        case "3":
            ChangeName(people);
            break;
        case "print":
            PrintResult(people);
            break;
        case "end":
            Environment.Exit(0);
            break;
        default:
            Console.WriteLine("输入错误");
            break;
    }
}

static void InputCheck(String? input)
{
    if (string.IsNullOrWhiteSpace(input))
    {
        throw new InputException("输入不为空");
    }
}

static void Add(List<School> people, member member)
{
    System.Console.WriteLine($"输入{member}姓名：");
    var name = Console.ReadLine();

    InputCheck(name);

    switch (member)
    {
        case member.Teacher:
            people.Add(new Teacher(name));
            break;
        case member.Student:
            System.Console.WriteLine("输入课程");
            var course = Console.ReadLine();
            InputCheck(course);
            people.Add(new Student(name, course));
            break;
    }
}

static void Remove(List<School> people)
{
    System.Console.WriteLine("输入删除的姓名");
    var inputRemove = Console.ReadLine();

    InputCheck(inputRemove);

    var peopleRemove = people.FirstOrDefault(p => p.Name.Equals(inputRemove, StringComparison.OrdinalIgnoreCase));

    if (peopleRemove == null)
    {
        System.Console.WriteLine($"不存在{inputRemove}");
        return;
    }

    people.Remove(peopleRemove);
    System.Console.WriteLine($"{peopleRemove.Name}已删除");
}

static void ChangeName(List<School> people)
{
    System.Console.WriteLine("输入要修改的名字");
    var oldName = Console.ReadLine();

    InputCheck(oldName);

    var peopleChange = people.FirstOrDefault(p => p.Name.Equals(oldName,StringComparison.OrdinalIgnoreCase));

    if(peopleChange == null)
    {
        System.Console.WriteLine($"{oldName}不存在");
        return;
    }

    System.Console.WriteLine("输入新名字");
    var newName = Console.ReadLine();
    InputCheck(newName);

    peopleChange.Name = newName;
    System.Console.WriteLine("修改成功");
}

static void PrintResult(List<School> people)
{
    if (people == null || people.Count == 0)
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

public class InputException : Exception
{
    public InputException(string message) : base(message) { }
}
