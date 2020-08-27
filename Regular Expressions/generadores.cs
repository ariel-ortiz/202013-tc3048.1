using System;
using System.Collections.Generic;

public class Generadores {

    public static IEnumerable<int> Ejemplo() {
        var n = 1;
        yield return n;
        n++;
        yield return n;
        n++;
        yield return n;
    }

    public static IEnumerable<int> Pow2(int max) {
        var n = 1;
        while (n < max) {
            yield return n;
            n *= 2;
        }
    }

    public static void Main() {

        var e = Ejemplo().GetEnumerator();
        while (e.MoveNext()) {
            Console.WriteLine(e.Current);
        }

        foreach (var x in Pow2(1000)) {
            Console.WriteLine(x);
        }
    }
}
