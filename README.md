# Endpoints da API de Gerenciamento de Tarefas

1. **Listagem de Projetos:**
   - Rota: `/api/v1/projeto/listar/:usuarioId`

2. **Visualização de Tarefas:**
   - Rota: `/api/v1/tarefa/listar/tarefas-do-projeto/:projetoId`

3. **Criação de Projetos:**
   - Rota: `/api/v1/projeto`

4. **Criação de Tarefas:**
   - Rota: `/api/v1/tarefa/:id?usuarioId=1&texto=texto de comentario`

5. **Atualização de Tarefas:**
   - Rota: `/api/v1/tarefa/:id/atualizar-status`

6. **Remoção de Tarefas:**
   - Rota: `/api/v1/tarefa/:id?usuarioId=1`

7. **Relatórios de Desempenho:**
   - Rota: `/api/v1/tarefa/listar/relatorios-de-desempenho/{usuarioId}`
   - Observação: A funcionalidade de relatórios de desempenho pode parecer paradoxal, uma vez que não há autenticação de usuário. Além disso, é recomendado que o acesso a esses relatórios seja restrito exclusivamente a usuários detentores de uma função específica, como a de "gerente".
   
   
   
   # Perguntas para Refinamento e Melhorias

1. **Escopo e Prioridades:**
   - Quais são as prioridades específicas para as próximas iterações?
   - Há funcionalidades adicionais que podem ser consideradas para futuras versões?
   - Existe alguma mudança no escopo do projeto?

2. **Segurança:**
   - Existe a necessidade de implementar medidas adicionais de segurança, especialmente se a API estiver exposta publicamente?
   - Como podemos garantir a integridade dos dados e proteger contra possíveis ataques?

3. **Desempenho e Escalabilidade:**
   - Há planos para escalar a aplicação no futuro, e existe alguma consideração específica para isso?

4. **Integrações Futuras:**
   - Como podemos tornar a API mais flexível para futuras integrações?



   # Postman

4. **Coleção Postman para Testes e Melhoria da Experiência**
	- A fim de promover uma experiência mais refinada e eficiente durante o desenvolvimento e teste da nossa API de Gerenciamento de Tarefas, apresento a coleção Postman abaixo. Esta coleção é projetada para facilitar o processo de teste, identificação de melhorias e validação de novas implementações. Por favor, utilize esta coleção como uma ferramenta valiosa para garantir a qualidade e robustez do nosso sistema.
  


# Execução do Docker para o Projeto




## Instruções de Execução

1. **Construa a Imagem Docker:**

   ```bash
   docker build -t eclipseworksapi:dev .
   docker run -p 8080:80 eclipseworksapi:dev




	