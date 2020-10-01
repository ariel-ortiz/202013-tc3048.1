/*
 * LL(1) Grammar
 *
 *  Prog ::= Exp "EOF"
 *  Exp  ::= Term ("+" Term)*
 *  Term ::= Fact ("*" Fact)*
 *  Fact ::= "int" | "(" Exp ")"
 */
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public enum TokenCategory {
    INT, PLUS, TIMES, OPEN_PAR, CLOSE_PAR, EOF, BAD_TOKEN
}

public class Token {
    public TokenCategory Category { get; }
    public String Lexeme { get; }

    public Token(TokenCategory category, String lexeme) {
        Category = category;
        Lexeme = lexeme;
    }

    public override String ToString() {
        return $"[{Category}, \"{Lexeme}\"]";
    }
}

public class Scanner {
    readonly String input;
    static readonly Regex regex = new Regex(
        @"(\d+)|([+])|([*])|([(])|([)])|(\s)|(.)");

    public Scanner(String input) {
        this.input = input;
    }

    public IEnumerable<Token> Start() {
        foreach (Match m in regex.Matches(input)) {
            if (m.Groups[1].Success) {
                yield return new Token(TokenCategory.INT, m.Value);
            } else if (m.Groups[2].Success) {
                yield return new Token(TokenCategory.PLUS, m.Value);
            } else if (m.Groups[3].Success) {
                yield return new Token(TokenCategory.TIMES, m.Value);
            } else if (m.Groups[4].Success) {
                yield return new Token(TokenCategory.OPEN_PAR, m.Value);
            } else if (m.Groups[5].Success) {
                yield return new Token(TokenCategory.CLOSE_PAR, m.Value);
            } else if (m.Groups[6].Success) {
                // skip
            } else if (m.Groups[7].Success) {
                yield return new Token(TokenCategory.BAD_TOKEN, m.Value);
            }
        }
        yield return new Token(TokenCategory.EOF, null);
    }
}

public class SyntaxError: Exception { }

public class Parser {
    IEnumerator<Token> tokenStream;

    public Parser(IEnumerator<Token> tokenStream) {
        this.tokenStream = tokenStream;
        this.tokenStream.MoveNext();
    }

    public TokenCategory Current {
        get {
            return tokenStream.Current.Category;
        }
    }

    public Token Expect(TokenCategory category) {
        if (Current == category) {
            Token current = tokenStream.Current;
            tokenStream.MoveNext();
            return current;
        } else {
            throw new SyntaxError();
        }
    }

    void Prog() {
        Exp();
        Expect(TokenCategory.EOF);
    }

    void Exp() {
        Term();
        while (Current == TokenCategory.PLUS) {
            Expect(TokenCategory.PLUS);
            Term();
        }
    }

    void Term() {
        Fact();
        while (Current == TokenCategory.TIMES) {
            Expect(TokenCategory.TIMES);
            Fact();
        }
    }

    void Fact() {
        switch(Current) {
        case TokenCategory.INT:
            Expect(TokenCategory.INT);
            break;
        case TokenCategory.OPEN_PAR:
            Expect(TokenCategory.OPEN_PAR);
            Exp();
            Expect(TokenCategory.CLOSE_PAR);
            break;
        default:
            throw new SyntaxError();
        }
    }
}

public class Driver {
    public static void Main() {
        Console.Write("> ");
        var line = Console.ReadLine();
        foreach (var token in new Scanner(line).Start()) {
            Console.WriteLine(token);
        }
    }
}