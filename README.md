# ProductRegistryAPI
API para realizar CRUD de Produtos disponibilizados pelos fornecedores. O projeto segue o modelo de pattern CQRS.

Tecnologias utilizadas:
ASP.NET Core 7.0
Entity Framework
C#
SQL Server

Para clonar e executar o projeto:
- cd caminho/do/seu/diretorio
- git clone https://github.com/joascorrea96/ProductRegistryAPI.git
- cd ProductRegistryAPI
- dotnet ef migrations add InitialCreate
- dotnet ef database update
- Executar Procedure abaixo via SQL Server: 

CREATE PROCEDURE GetProductsBySupplier
    @SupplierId INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        Id,
        Description,
        Brand,
        UnitOfMeasure,
        PhotoUrl,
        SupplierId
    FROM 
        Products
    WHERE 
        SupplierId = @SupplierId;
END;
