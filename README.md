# BlogSimples

## Instruções para Configuração e Migração do Banco de Dados

### 1. Ajustar a ConnectionString
Antes de rodar as migrações, é necessário ajustar a **ConnectionString** no arquivo `appsettings.json` para corresponder ao seu ambiente de banco de dados.

### 2. Executar as Migrações
Para aplicar as migrações e atualizar o banco de dados, siga os passos abaixo:

1. Abra o **Console do Gerenciador de Pacotes** no Visual Studio.
2. No console, execute o comando para adicionar uma nova migração. Substitua `<qualquer nome>` pelo nome desejado para a migração:
   ```
   add-migration <qualquer nome>
   ```
3. Após adicionar a migração, execute o seguinte comando para aplicar as migrações e atualizar o banco de dados:
   ```
   update-database
   ```
Isso irá gerar as alterações no banco de dados com base nas migrações criadas.


## 1. Orientação a Objetos

### Conceito de Herança Múltipla e Abordagem no C#
Herança múltipla é um conceito da programação orientada a objetos que permite que uma classe herde atributos e métodos de mais de uma classe base.  
- **C# não suporta herança múltipla direta de classes.**  
- Mecanismos alternativos no C#:
  - **Interfaces**: Permitem simular herança múltipla definindo contratos implementados por várias classes.
  - **Composição**: Uso de objetos como membros de classe para reutilizar funcionalidades.
  - **Classes Abstratas e Interfaces**: Combinando esses conceitos para resolver cenários específicos.

### Polimorfismo em C#
Polimorfismo é a capacidade de tratar objetos de classes diferentes de forma uniforme.  
- **Polimorfismo Estático (Sobrecarga de Métodos)**: Define múltiplos métodos com o mesmo nome mas assinaturas diferentes.
- **Polimorfismo Dinâmico (Sobrescrita de Métodos)**: Permite que classes derivadas redefinam métodos da classe base.

Exemplo prático de polimorfismo dinâmico:
```csharp
public abstract class Transport {
    public abstract void Move();
}

public class Car : Transport {
    public override void Move() {
        Console.WriteLine("The car drives on the road.");
    }
}

public class Plane : Transport {
    public override void Move() {
        Console.WriteLine("The plane flies in the sky.");
    }
}

public class Boat : Transport {
    public override void Move() {
        Console.WriteLine("The boat sails on water.");
    }
}

public class Program {
    public static void Main() {
        List<Transport> transports = new List<Transport> {
            new Car(),
            new Plane(),
            new Boat()
        };
        foreach (var transport in transports) {
            transport.Move();
        }
    }
}
```

## 2. SOLID

### Princípio da Responsabilidade Única (SRP)
O **Princípio da Responsabilidade Única (Single Responsibility Principle - SRP)** é um dos cinco princípios do SOLID e estabelece que uma classe deve ter apenas uma razão para mudar, ou seja, deve ser responsável por apenas uma única parte da funcionalidade do sistema.

- **Aplicação em C#:** O SRP pode ser aplicado ao projetar classes e métodos especializados em uma única responsabilidade. Caso uma classe comece a acumular múltiplas responsabilidades, ela deve ser refatorada em várias classes menores, cada uma focada em uma única tarefa.

### Princípio da Inversão de Dependência (DIP)
O DIP pode ser aplicado criando uma abstração e desacoplando classes da implementação concreta.

- **Benefícios:**
  - Melhora a modularidade.
  - Facilita o teste.
  - Promove um código mais robusto e preparado para mudanças.

---

## 3. Entity Framework (EF)

### Como o Entity Framework gerencia o mapeamento de objetos para o banco de dados e vice-versa?
O EF realiza o mapeamento de objetos para tabelas e de propriedades para colunas usando três abordagens principais:

1. **Code First:** Classes e relacionamentos definidos no código geram automaticamente o banco de dados.
2. **Database First:** Um banco de dados existente gera as classes de modelo.
3. **Model First:** Um modelo gráfico (EDMX) gera as classes e o banco de dados.

### Como otimizar consultas no Entity Framework para grandes conjuntos de dados?
1. **Evitar o Carregamento Desnecessário de Dados.**
2. **Paginação:** Nunca carregue todos os registros de uma vez.
3. **Filtros:** Aplique filtros no banco antes de carregar os dados.
4. **Consultas Assíncronas.**
5. **Indexação no Banco de Dados.**
6. **Carregamento Otimizado:** Use Split Queries ou filtros em dados relacionados.
7. **Desabilitar Rastreamento para Consultas Somente Leitura.**
8. **Evitar Projeções Complexas.**
9. **Consultar Apenas os Dados Necessários.**
10. **Procedimentos Armazenados ou SQL Bruto.**
11. **Uso de Parâmetros.**
12. **Cacheamento de Resultados.**

---

## 4. WebSockets

### Papel dos WebSockets em uma aplicação C#
WebSockets, implementados no ASP.NET Core, permitem que o servidor envie e receba mensagens diretamente de um cliente sem a necessidade de repetidas solicitações HTTP. São mais eficientes do que comunicações baseadas em HTTP.

- **WebSockets:** Ideais para comunicações contínuas e de baixa latência.
- **HTTP/REST:** Melhor para operações ocasionais, como acessar APIs.

### Considerações de Segurança ao Implementar WebSockets
1. Usar conexões seguras (WSS).
2. Autenticação e autorização.
3. Proteção contra ataques DoS.
4. Gerenciar encerramento de conexões.
5. Validação de dados recebidos.
6. Configurar cabeçalhos de segurança.
7. Configurar Cross-Origin Resource Sharing (CORS).
8. Monitoramento e logging.

---

## 5. Arquitetura

### Diferença entre Arquitetura Monolítica e Microsserviços
- **Arquitetura Monolítica:** Sistema único e coeso com componentes fortemente acoplados.
- **Arquitetura de Microsserviços:** Divisão em serviços independentes, cada um com responsabilidades específicas.

### Escolha para Projetar uma Aplicação C#
- **Monolítica:** Ideal para projetos simples, como sistemas internos ou MVPs.
- **Microsserviços:** Melhor para aplicações grandes e complexas, onde se espera crescimento escalável.

### Qual escolher para alta escalabilidade?
A **arquitetura de microsserviços** é ideal devido à capacidade de escalar componentes de forma independente. Para uma aplicação altamente escalável, escolheria microsserviços.
