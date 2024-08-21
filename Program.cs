internal class Program
{
    private static void Main(string[] args)
    {
        Menu();
    }

    static void Menu()
    {
        Console.Clear();
        Console.WriteLine("O que você deseja fazer?");
        Console.WriteLine("1 - Abrir arquivo");
        Console.WriteLine("2 - Criar novo arquivo");
        Console.WriteLine("0 - Sair");
        short option;

        if (short.TryParse(Console.ReadLine(), out option))
        {
            switch (option)
            {
                case 0: Environment.Exit(0); break;
                case 1: Open(); break;
                case 2: Edit(); break;
                default: Menu(); break;
            }
        }
        else
        {
            Console.WriteLine("Opção inválida. Tente novamente.");
            Thread.Sleep(2000);
            Menu();
        }
    }

    static void Open()
    {
        Console.Clear();
        Console.WriteLine("Qual o caminho do arquivo?");
        string path = Console.ReadLine();

        if (File.Exists(path))
        {
            try
            {
                using (var file = new StreamReader(path))
                {
                    string text = file.ReadToEnd();
                    Console.WriteLine(text);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao abrir o arquivo: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine("Arquivo não encontrado.");
        }

        Console.WriteLine("");
        Console.WriteLine("Pressione Enter para voltar ao menu.");
        Console.ReadLine();
        Menu();
    }

    static void Edit()
    {
        Console.Clear();
        Console.WriteLine("Digite abaixo (ESC para sair)\n");
        Console.WriteLine("--------------------------");
        string text = "";

        do
        {
            string input = Console.ReadLine();
            text += input + Environment.NewLine;
        }
        while (Console.KeyAvailable == false && Console.ReadKey(true).Key != ConsoleKey.Escape);

        Console.Clear();
        Console.WriteLine("Deseja salvar o arquivo? (S/N)");
        if (Console.ReadLine().ToLower() == "s")
        {
            Salvar(text);
        }
        else
        {
            Menu();
        }
    }

    static void Salvar(string text)
    {
        Console.Clear();
        Console.WriteLine("Qual o caminho para salvar o arquivo?");
        var path = Console.ReadLine();

        try
        {
            using (var file = new StreamWriter(path))
            {
                file.Write(text);
            }

            Console.WriteLine($"Arquivo {path} salvo com sucesso!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao salvar o arquivo: {ex.Message}");
        }

        Console.WriteLine("Pressione Enter para voltar ao menu.");
        Console.ReadLine();
        Menu();
    }
}
