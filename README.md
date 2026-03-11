# 🛍️ Jam Imports - Sistema de Mensageria e Microsserviços

Este projeto é uma prova de conceito (PoC) de uma arquitetura baseada em microsserviços e mensageria assíncrona, desenvolvida para o ecossistema da loja de roupas Jam Imports.

O objetivo principal é demonstrar a interoperabilidade entre diferentes ecossistemas (.NET e Java) comunicando-se de forma resiliente através do Apache Kafka.

## 🏗️ Arquitetura do Sistema

A solução foi desenhada para garantir que o cadastro de novos produtos não fique bloqueado por processos em segundo plano, utilizando o padrão **Producer/Consumer**.

1. **Producer (.NET 9 API):** Responsável por receber a requisição HTTP, persistir os dados da peça de roupa no banco de dados relacional e publicar um evento de "Novo Produto" no mensageiro.
2. **Message Broker (Apache Kafka):** Atua como o coração da comunicação assíncrona, rodando em modo moderno KRaft (sem Zookeeper), garantindo a entrega da mensagem.
3. **Consumer (Java Spring Boot):** Serviço independente que escuta o tópico em tempo real. Ao detectar um novo cadastro, consome a mensagem para processamento posterior (ex: cálculo de impostos, notificação de estoque, etc).
4. **Database (PostgreSQL):** Banco de dados relacional para persistência dos produtos.

## 🚀 Tecnologias Utilizadas

* **Backend API:** C# / .NET 9
* **Backend Worker:** Java 21 / Spring Boot 4
* **Mensageria:** Apache Kafka (Apache Official Image - KRaft mode)
* **Banco de Dados:** PostgreSQL
* **Infraestrutura:** Docker & Docker Compose
* **ORM:** Entity Framework Core

### Pré-requisitos
* [Docker Desktop](https://www.docker.com/products/docker-desktop) instalado e rodando.
* [.NET 9 SDK](https://dotnet.microsoft.com/download)
* [Java 21 e Maven](https://maven.apache.org/) (ou a IDE de sua preferência, como IntelliJ)
