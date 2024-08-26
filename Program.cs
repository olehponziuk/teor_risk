//LB1
//Ponziuk Oleh

double choice;
try {
    choice = Choice();
}
catch(TableException msg)
{
    choice = 0;
    Console.WriteLine(msg);
}
Console.WriteLine(choice);

double Choice()
{
    string? var1, var2, Event;
    Console.WriteLine("Enter name of first variant: ");
    var1 = Console.ReadLine();
    Console.WriteLine("Enter name of second variant: ");
    var2 = Console.ReadLine();
    Console.WriteLine("Enter bad event: ");
    Event = Console.ReadLine();
    double P;
    float[] tab = new float[5];
    Console.WriteLine($"Введіть вірогідність {Event}");
    P = Convert.ToDouble(Console.ReadLine());

    Console.WriteLine($"Поставте відповідність:");
    string[] rating = new string[5]
    {
        "дуже добре",
        "добре",
        "нормально",
        "погано",
        "дуже погано"
    };
    Console.Write($"{rating[0]}: ");
    tab[0] = (float)Convert.ToDouble(Console.ReadLine());
    for(int i = 1; i < 5; ++i) {
        Console.Write($"{rating[0]}: ");
        tab[i] = (float)Convert.ToDouble(Console.ReadLine());
        if ( (tab[i] - tab[i-1]) > 0.0) 
            throw new TableException("ERROR TABLE");
    }
            
    double var1_will_be_event, var1_will_not_event,
           var2_will_be_event, var2_will_not_event;
    double u1, u2;

    Console.WriteLine($"Введіть оцінку ситуації, коли {var1} і {Event}");
    var1_will_be_event  = Convert.ToDouble(Console.ReadLine());
    Console.WriteLine($"Введіть оцінку ситуації, коли {var1} і не {Event}");
    var1_will_not_event = Convert.ToDouble(Console.ReadLine());
    Console.WriteLine($"Введіть оцінку ситуації, коли {var2} і {Event}");
    var2_will_be_event = Convert.ToDouble(Console.ReadLine());
    Console.WriteLine($"Введіть оцінку ситуації, коли {var2} і не {Event}");
    var2_will_not_event = Convert.ToDouble(Console.ReadLine());
    
    u1 = var1_will_be_event * P + var1_will_not_event * (1.0 - P);
    u2 = var2_will_be_event * P + var2_will_not_event * (1.0 - P);
    
    Console.WriteLine($"U({var1}) = {var1_will_be_event} * {P} + {var1_will_not_event} * (1.0 - {P}) = {u1}");
    Console.WriteLine($"U({var2}) = {var2_will_be_event} * {P} + {var2_will_not_event} * (1.0 - {P}) = {u2}");

    if (u1 > u2) {
        Console.WriteLine($"U({var1}) > U({var2})");
        Console.WriteLine($"Рекомендується дотримуватися сценарію {var1}");
    }
    else if (u1 < u2) {
        Console.WriteLine($"U({var2}) > U({var1})");
        Console.WriteLine($"Рекомендується дотримуватися сценарію {var2}");
    }
    else{
        Console.WriteLine($"U({var2}) == U({var1})");
        Console.WriteLine("Можна вже і самим прийняти рішення!");
    }

    return u1 - u2;
}

public class TableException : System.Exception
{
    public TableException() { }
    public TableException(string message) : base(message) { }
}