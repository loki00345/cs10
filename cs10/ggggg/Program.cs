using System;

public class Money
{
    public int Hryvnias { get; private set; }
    public int Kopiykas { get; private set; }

    public Money(int hryvnias, int kopiykas)
    {
        if (hryvnias < 0 || kopiykas < 0)
            throw new ArgumentException("Сума не може бути від'ємною.");
        Hryvnias = hryvnias + kopiykas / 100;
        Kopiykas = kopiykas % 100;
    }

    public override string ToString()
    {
        return $"{Hryvnias} грн {Kopiykas} коп.";
    }

    public static Money operator +(Money m1, Money m2)
    {
        return new Money(m1.Hryvnias + m2.Hryvnias, m1.Kopiykas + m2.Kopiykas);
    }

    public static Money operator -(Money m1, Money m2)
    {
        int total1 = m1.Hryvnias * 100 + m1.Kopiykas;
        int total2 = m2.Hryvnias * 100 + m2.Kopiykas;

        if (total1 < total2)
            throw new InvalidOperationException("Банкрут");

        int totalResult = total1 - total2;
        return new Money(totalResult / 100, totalResult % 100);
    }

    public static Money operator *(Money m, int multiplier)
    {
        if (multiplier < 0)
            throw new ArgumentException("Множник не може бути від'ємним.");

        int total = (m.Hryvnias * 100 + m.Kopiykas) * multiplier;
        return new Money(total / 100, total % 100);
    }

    public static Money operator /(Money m, int divisor)
    {
        if (divisor <= 0)
            throw new ArgumentException("Дільник повинен бути більшим за нуль.");

        int total = m.Hryvnias * 100 + m.Kopiykas;
        return new Money(total / divisor / 100, total / divisor % 100);
    }

    public static Money operator ++(Money m)
    {
        return new Money(m.Hryvnias, m.Kopiykas + 1);
    }

    public static Money operator --(Money m)
    {
        if (m.Hryvnias == 0 && m.Kopiykas == 0)
            throw new InvalidOperationException("Банкрут");
        return new Money(m.Hryvnias, m.Kopiykas - 1);
    }

    public static bool operator <(Money m1, Money m2)
    {
        return m1.Hryvnias * 100 + m1.Kopiykas < m2.Hryvnias * 100 + m2.Kopiykas;
    }

    public static bool operator >(Money m1, Money m2)
    {
        return m1.Hryvnias * 100 + m1.Kopiykas > m2.Hryvnias * 100 + m2.Kopiykas;
    }

    public static bool operator ==(Money m1, Money m2)
    {
        return m1.Hryvnias == m2.Hryvnias && m1.Kopiykas == m2.Kopiykas;
    }

    public static bool operator !=(Money m1, Money m2)
    {
        return !(m1 == m2);
    }

    public override bool Equals(object obj)
    {
        if (obj is Money other)
            return this == other;
        return false;
    }

    public override int GetHashCode()
    {
        return Hryvnias * 100 + Kopiykas;
    }
}

class Program
{
    static void Main()
    {
        try
        {

            Console.Write("Введіть кількість гривень для першої суми: ");
            int hryvnias1 = int.Parse(Console.ReadLine());

            Console.Write("Введіть кількість копійок для першої суми: ");
            int kopiykas1 = int.Parse(Console.ReadLine());


            Console.Write("Введіть кількість гривень для другої суми: ");
            int hryvnias2 = int.Parse(Console.ReadLine());

            Console.Write("Введіть кількість копійок для другої суми: ");
            int kopiykas2 = int.Parse(Console.ReadLine());

            Money m1 = new Money(hryvnias1, kopiykas1);
            Money m2 = new Money(hryvnias2, kopiykas2);

            Console.WriteLine($"Сума m1: {m1}");
            Console.WriteLine($"Сума m2: {m2}");

            Console.WriteLine($"m1 + m2 = {m1 + m2}");
            Console.WriteLine($"m1 - m2 = {m1 - m2}");
            Console.WriteLine($"m1 * 3 = {m1 * 3}");
            Console.WriteLine($"m1 / 2 = {m1 / 2}");

            Console.WriteLine($"m1 < m2: {m1 < m2}");
            Console.WriteLine($"m1 > m2: {m1 > m2}");
            Console.WriteLine($"m1 == m2: {m1 == m2}");
            Console.WriteLine($"m1 != m2: {m1 != m2}");

            m1++;
            Console.WriteLine($"m1 після ++: {m1}");

            m1--;
            Console.WriteLine($"m1 після --: {m1}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Помилка: {ex.Message}");
        }
    }

}
