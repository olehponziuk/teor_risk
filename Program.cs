/*4. Особа має функцію корисності U(x) = 0,01x2
   . Вона має три альтернативних варіанти вибору нового місця роботи. Перше місце роботи пов’язане
   зі стабільною заробітною платнею у 2000 гр.од. Друге місце роботи
   пов’язане з ризиком: або мати заробітну платню 3000 гр.од. з ймовірністю
   0,5, або заробітну платню 1000 гр.од. Третє місце роботи також пов’язане з
   ризиком мати 4000 гр.од. з ймовірністю 0,5 або не мати заробітної платні
   взагалі. Яке місце роботи доцільно обрати цій особі?
*/

using System.ComponentModel.DataAnnotations;

double[] RA = new double[]{2000.0};
double[] PA = new double[]{1.0};

double[] RB = new double[]{1000.0, 3000.0};
double[] PB = new double[]{0.5, 0.5};

double[] RC = new double[]{4000.0, 0.0};
double[] PC = new double[] { 0.5, 0.5 };

Console.WriteLine("Ми маємо 3 компанії: А В С");
Console.WriteLine($"Для А ми немаємо ризику. Отже, немаємо лотереї L({RA[0]}, {PA[0]}, !) ");
Console.WriteLine($"Для B маємо лотерею L({RB[0]}, {PB[0]}, {RB[1]}) ");
Console.WriteLine($"Для B маємо лотерею L({RC[0]}, {PC[0]}, {RC[1]}) ");

Ufunc? fU = (double x) => 0.01 * x * x;
Ufunc? reversU = (double U) => Math.Sqrt(U/0.01);

double MA = ExpectedGain(RA, PA);
double MB = ExpectedGain(RB, PB);
double MC = ExpectedGain(RC, PC);

Console.WriteLine($"\nСподіваний виграш для А = {MA},");
Console.WriteLine($"Сподіваний виграш для B = {MB},");
Console.WriteLine($"Сподіваний виграш для C = {MC}");

double EUA = ExpectedUsefulness(RA, PA, fU);
double EUB = ExpectedUsefulness(RB, PB, fU);
double EUC = ExpectedUsefulness(RC, PC, fU);

Console.WriteLine($"\nСподівана корисність для А = {EUA},");
Console.WriteLine($"Сподівана корисність для B = {EUB},");
Console.WriteLine($"Сподівана корисність для C = {EUC}");

char bestCompEU;
if (EUC >= EUB && EUC >= EUA)
    bestCompEU = 'C';
else if (EUB >= EUA && EUB >= EUC)
    bestCompEU = 'B';
else
    bestCompEU = 'A';

Console.WriteLine($"\nНайкраща лотерея по Сподіваній корисності - {bestCompEU}");

double DEA = DeterministicEquivalent(EUA, reversU);
double DEB = DeterministicEquivalent(EUB, reversU);
double DEC = DeterministicEquivalent(EUC, reversU);

Console.WriteLine($"\nДетермінований еквівалент для А = {DEA},");
Console.WriteLine($"Детермінований еквівалент для B = {DEB},");
Console.WriteLine($"Детермінований еквівалент для C = {DEC}");

double piA = RiskForPremium(MA, DEA);
double piB = RiskForPremium(MB, DEB);
double piC = RiskForPremium(MC, DEC);

Console.WriteLine($"\nПремія за ризик для А = {piA},");
Console.WriteLine($"Премія за ризик для B = {piB},");
Console.WriteLine($"Премія за ризик для C = {piC}");

if (piC >= piB && piC >= piA)
    bestCompEU = 'C';
else if (piB >= piA && piB >= piC)
    bestCompEU = 'B';
else
    bestCompEU = 'A';

Console.WriteLine($"\nОтже, найменш ризикованим є вибір - {bestCompEU}");




double RiskForPremium(double EG, double DE) => EG - DE;

double DeterministicEquivalent(double U, Ufunc? reversU) => (double)reversU?.Invoke(U)!;

double ExpectedUsefulness(double[] R, double[] P, Ufunc? U)
{
    if (R.Length != P.Length)
        throw new LengthException("ExpectedUsefulness: LEN ERROR for R and P");

    int n = R.Length;
    double resSum = 0;
    for (int i = 0; i < n; i++)
        resSum += P[i] * U(R[i]);
    
    return resSum;
}

double ExpectedGain(double[] R, double[] P)
{
    if (R.Length != P.Length)
        throw new LengthException("ExpectedGain: LEN ERROR for R and P");

    int n = R.Length;
    double resSum = 0;
    for (int i = 0; i < n; i++)
        resSum += P[i] * R[i];
    
    return resSum;
}

delegate double Ufunc(double x);


public class LengthException : System.Exception
{
    public LengthException() { }
    public LengthException(string message) : base(message) { }
}
