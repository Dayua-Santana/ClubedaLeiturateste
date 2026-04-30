


```markdown
# 📚 Clube da Leitura

Sistema de console em C# para gerenciar um clube de leitura de revistas, controlando caixas de armazenamento, amigos do clube e empréstimos.


![alt text](ConsoleApp_GNBmA4KGO9.gif)


## 🎯 Funcionalidades

- **Gerenciar Caixas**: cadastrar, visualizar, editar e excluir caixas onde as revistas são organizadas (por cor, etiqueta e dias de empréstimo permitidos).
- **Gerenciar Amigos**: cadastrar, visualizar, editar e excluir os amigos do clube com nome, responsável e telefone.
- **Gerenciar Revistas**: cadastrar, visualizar, editar e excluir revistas vinculadas a uma caixa, com título, edição e ano de publicação.
- **Gerenciar Empréstimos**: registrar empréstimos de revistas para amigos, com cálculo automático da data de devolução prevista a partir dos dias da caixa.

## 🛠️ Tecnologias

- **Linguagem:** C#
- **Framework:** .NET 10
- **Tipo:** Aplicação de Console

## 📁 Estrutura do Projeto

```
ConsoleApp/
├── Dominio/             # Entidades do domínio (Caixa, Revista, Amigo, Emprestimo)
├── Apresentacao/        # Telas de interação com o usuário
├── Infraestrutura/      # Repositórios para persistência em memória
└── Program.cs           # Ponto de entrada da aplicação
```

A aplicação segue uma separação em camadas:
- **Domínio:** regras de negócio e entidades.
- **Apresentação:** menus e interação no console.
- **Infraestrutura:** armazenamento dos registros em memória.

## ▶️ Como Executar

Pré-requisitos: [.NET SDK 10.0](https://dotnet.microsoft.com/download) instalado.

```bash
# Clonar o repositório
git clone https://github.com/Dayua-Santana/ClubedaLeiturateste.git

# Entrar na pasta do projeto
cd ClubedaLeiturateste/ConsoleApp

# Executar
dotnet run
```

## 📋 Fluxo de Uso

1. Cadastre uma ou mais **Caixas** (definindo cor, etiqueta e dias de empréstimo).
2. Cadastre os **Amigos** do clube.
3. Cadastre as **Revistas**, vinculando cada uma a uma caixa.
4. Registre **Empréstimos** selecionando o amigo e a revista desejada.
