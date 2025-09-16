using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem;

public class Parent 
{
    private readonly string _name;
    public Parent(string name)
    {
        _name = name;
    }
    public Parent()
    {

    }
    public virtual string GetName()
    {
        return $"ParentName : ";
    }
}

public class Child : Parent
{
    //public Child(string name) : base(name)
    //{
        
    //}
    public string GetChildName()
    {
        return $"ChildName : {GetName()}";
    }
    public override string GetName()
    {
               return $"ChildName : ";
    }
}

public interface IParent
{
    string GetName();
}
public interface IChild
{
    string GetName();
}
public class GrandChild : IParent, IChild
{
    public string GetName()
    {
        throw new NotImplementedException();
    }
}

class Result
{
   public static string Test()
    {
        var g = new GrandChild();
        var name = g.GetName();
        return name;
    }
}