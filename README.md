# exercicio-api
API criada para exercitar os conhecimentos adquiridos

Para testar iniciar o GET do controller Start.

Vai dar origem aos usuários:

{ 
  Codigo = 1, Nome = "administrador", 
  Senha = "123", 
  Cargo = "administrador" 
}
{ 
  Codigo = 2, 
  Nome = "gerente", 
  Senha = "123", 
  Cargo = "gerente" 
}
{ Codigo = 3, 
  Nome = "funcionario", 
  Senha = "123", 
  Cargo = "funcionario" 
}


Para acessar um método autenticado, realizar login, exemplo: gerente, 123.
Copiar o token.

Abrir o método Inserir, em categorias, e usar o token copiado na key Authorization do Header.
Passar os dados da categoria a ser criada.
