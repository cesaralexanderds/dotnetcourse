#region menu

string opcion = "0";
int numeroRetiros = 0;
int idRetiro = 1;
int nuevoValorRetiro = 0;



Dictionary<int, int> retiros = new Dictionary<int, int>();

do
{
    Console.WriteLine("\n\t------------ Bienvenido al Banco CDIS ------------\n\n");
    Console.WriteLine("1. Ingresar la cantidad de retiros hechos por los usuarios.");
    Console.WriteLine("2. Revisar la cantidad entregada de billetes y monedas.");
    Console.WriteLine("3. Salir del programa");

    Console.WriteLine("\n Ingresa la opcion: ");
    opcion = Console.ReadLine();

    switch (opcion)
    {
        case "1":
            do
            {
                Console.WriteLine("¿Cuántos retiros se hicieron? (máximo 10)");
                numeroRetiros = int.Parse(Console.ReadLine());
            } while (numeroRetiros > 10 || numeroRetiros < 1);

            for (int i = 0; i < numeroRetiros; i++)
            {
                Console.WriteLine($"Ingresa la cantidad del retiro {i+1}");
                int cantidadRetiros = int.Parse(Console.ReadLine());
                if (cantidadRetiros <= 50000 && cantidadRetiros > 1)
                {
                    
                    retiros.Add(idRetiro++, cantidadRetiros);
                }
                else
                {
                    Console.WriteLine("Cantidad de retiro inválida.");
                    i--; 
                }
                
            }
            break;
        case "2":
            Console.Clear();
            foreach (var retiro in retiros)
                {   
                    Console.WriteLine($"El movimiento {retiro.Key} se retiró como: ");
                    if(retiro.Value / 500 != 0){
                            int billete500 = retiro.Value / 500;
                            nuevoValorRetiro =  retiro.Value - (billete500 * 500);
                            Console.WriteLine($"Se usaron {billete500} billetes de 500");
                    }else
                    if(nuevoValorRetiro / 200 != 0){
                            int billete200 = nuevoValorRetiro / 200;
                            nuevoValorRetiro =  nuevoValorRetiro - (billete200 * 200);
                            Console.WriteLine($"Se usaron {billete200} billetes de 200");
                    }else
                    if(nuevoValorRetiro / 100 != 0){
                            int billete100 = nuevoValorRetiro / 100;
                            nuevoValorRetiro =  nuevoValorRetiro - (billete100 * 100);
                            Console.WriteLine($"Se usaron {billete100} billetes de 100");
                    }else
                    if(nuevoValorRetiro / 50 != 0){
                            int billete50 = nuevoValorRetiro / 50;
                            nuevoValorRetiro =  nuevoValorRetiro - (billete50 * 50);
                            Console.WriteLine($"Se usaron {billete50} billetes de 50");
                    }else
                    if(nuevoValorRetiro / 20 != 0){
                            int billete20 = nuevoValorRetiro / 20;
                            nuevoValorRetiro =  nuevoValorRetiro - (billete20 * 20);
                            Console.WriteLine($"Se usaron {billete20} billetes de 20");
                    }else
                    if(nuevoValorRetiro / 10 != 0){
                            int moneda10 = nuevoValorRetiro / 10;
                            nuevoValorRetiro =  nuevoValorRetiro - (moneda10 * 10);
                            Console.WriteLine($"Se usaron {moneda10} monedas de 10");
                    }else
                    if(nuevoValorRetiro / 5 != 0){
                            int moneda5 = nuevoValorRetiro / 5;
                            nuevoValorRetiro =  nuevoValorRetiro - (moneda5 * 5);
                            Console.WriteLine($"Se usaron {moneda5} monedas de 5");

                    }else
                    if(nuevoValorRetiro / 1 != 0){
                            int moneda1 = nuevoValorRetiro / 1;
                            nuevoValorRetiro =  nuevoValorRetiro - (moneda1 * 1);
                            Console.WriteLine($"Se usaron {moneda1} monedas de 1");
                    }

                }
            Console.WriteLine("Presiona Enter para continuar...");
            Console.ReadLine();
            Console.Clear();
            break;
    }
} while (opcion != "3");

#endregion
