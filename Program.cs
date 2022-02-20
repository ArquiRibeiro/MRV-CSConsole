// See https://aka.ms/new-console-template for more information

using System;

namespace CRUD{
    class Program{
        static SerieRepositorio repositorio = new SerieRepositorio();
        static void Main(string[] args){
            string opcaoUsuario = ObterOpcaoUsuario();

            while (opcaoUsuario.ToUpper() != "X"){
                switch (opcaoUsuario){
                    case "1":
                        ListarSeries();
                        break;
                    case "2":
                        InserirSerie();
                        break;
                    case "3":
                        //AtualizarSerie();
                        break;
                    case "4":
                        ExcluirSerie();
                        break;
                    case "5":
                        VisualizarSerie();
                        break;
                    case "C":
                        Console.Clear();
                        break;
                    
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                opcaoUsuario = ObterOpcaoUsuario();
            }

            Console.WriteLine("Obrigado por testar o programa!");
        }

        private static void ListarSeries(){
            Console.WriteLine("Listar séries");

            var lista = repositorio.Lista();

            if (lista.Count == 0){
                Console.WriteLine("Nenhuma série cadastrada.");
            }

            foreach(var serie in lista){
                var excluido = serie.retornaExcluido();
                if(!excluido){
                    Console.WriteLine("#ID {0}: - {1}", serie.retornaId(), serie.retornaTitulo());
                }
                else{
                    Console.WriteLine("#ID {0}: Excluido", serie.retornaId());
                }
            }
        }

        private static void InserirSerie(){
            Console.WriteLine("Inserir nova série");

            foreach(int i in Enum.GetValues(typeof(Genero))){
                Console.WriteLine("{0}={1}", i, Enum.GetName(typeof(Genero), i));
            }
            Console.Write("Digite o Gênero entre as opções listadas: ");
            int entradaGenero = int.Parse(Console.ReadLine());

            Console.Write("Digite o Título da Série: ");
            string entradaTitulo = Console.ReadLine();

            Console.Write("Digite o Ano em que a série foi ao ar: ");
            int entradaAno = int.Parse(Console.ReadLine());

            Console.Write("Digite uma descrição para a série: ");
            string entradaDescricao = Console.ReadLine();

            Serie novaSerie = new Serie(
                id: repositorio.ProximoId(),
                genero: (Genero)entradaGenero,
                titulo: entradaTitulo,
                descricao: entradaDescricao,
                ano: entradaAno);    
            
            repositorio.Insere(novaSerie);
        }

        private static void ExcluirSerie(){
            Console.Write("Digite o ID da série a ser Excluida: ");
            int indiceSerie = int.Parse(Console.ReadLine());

            repositorio.Exclui(indiceSerie);
        }

        private static void VisualizarSerie(){
            Console.Write("Digite o ID da série a ser visualizada");
            int indiceSerie = int.Parse(Console.ReadLine());

            var serie = repositorio.RetornaPorId(indiceSerie);
            Console.WriteLine(serie);
        }

        private static string ObterOpcaoUsuario(){
            Console.WriteLine();
            Console.WriteLine("Informe a opção desejada:");

            Console.WriteLine("1- Listar Series");
            Console.WriteLine("2- Inserir uma nova série");
            Console.WriteLine("3- Atualizar uma série");
            Console.WriteLine("4- Excluir uma série");
            Console.WriteLine("5- Visualizar série");
            Console.WriteLine("C- Limpar terminal");
            Console.WriteLine("X- Terminar");
            Console.WriteLine();

            string opcaoUsuario = Console.ReadLine().ToUpper();
            Console.WriteLine();
            return opcaoUsuario;
        }
    }
}