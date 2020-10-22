using System;

public class Example {

    public static void M(int x) {
        Console.WriteLine("M(int)");
    }

    public static void M(string x) {
        Console.WriteLine("M(string)");
    }

    public static void Main() {
        dynamic x;
        x = "hola";
        M(x);
        x = 1;
        M(x);
    }
}