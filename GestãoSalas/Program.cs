var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

Aluno al1 = new Aluno(13241,"João");
Aluno al2 = new Aluno(14241, "Maria");
Aluno al3 = new Aluno(34241, "Manuel");

Sala a1 = new Sala('A', 1, 2);
a1.entrar(al1);
a1.entrar(al2);
a1.entrar(al3);

app.MapGet("/teste1", () => a1.entrar(al1) + a1.entrar(al2) + a1.sair(al1) + a1.entrar(al3));
app.MapGet("/", () => a1.listaAlunos() );

app.Run();

/*
 * Classe Aluno recebe os parametros numero e nome e permite criar Alunos
 * com base no número e nome
*/
class Aluno {
    //Atributos da classe
    int numero;
    //O public permite o acesso ao nome fora desta classe
    public string nome;

    //Constructor
    public Aluno(int numero,string nome)
    {
        this.numero = numero;
        this.nome = nome;
    }

    //Métodos
    public string toString()
    {
        return "Nome:"+nome+"\nNúmero:"+numero+"\n";
    }

}

class Sala {
    char bloco;
    int  numero;
    int  capacidade;

    List<Aluno> presentes = new List<Aluno>();

    public Sala(char bloco,int numero,int capacidade)
    {
        this.bloco = bloco;
        this.numero = numero;
        this.capacidade = capacidade;
    }

    public string entrar(Aluno al)
    {
        //O aluno só vai puder entrar quando ele ainda não lá estiver
        //e também quando a capacidade da sala o permitir.
        if(presentes.Count < capacidade)
        {
            if (presentes.Contains(al))
            {
                return "O Aluno já se encontra na sala!.\n";
            }
            else
            {
                presentes.Add(al);
                return "O Aluno " + al.nome + " entrou na sala " + bloco + "" + numero + ".\n";
            }
        }
        else
        {
            return "Capacidade da sala excedida.\n";
        }
    }

    public string sair(Aluno al)
    {
        if (presentes.Contains(al))
        {
            presentes.Remove(al);
            return "O Aluno " + al.nome + " saiu da sala.\n";
        }
        else
        {
            return "O Aluno não está na sala.\n";
        }
    }

    public string listaAlunos()
    {
        string result = "Lista de presenças:\n\n";

        foreach(Aluno al in presentes)
        {
            result += al.toString() + "\n";
        }

        return result;
        
    }
}