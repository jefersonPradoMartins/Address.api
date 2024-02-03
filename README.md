# Address API REST 

![.Net](https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white)
![Redis](https://img.shields.io/badge/Redis-DC382D?style=for-the-badge&logo=redis&logoColor=white)
![Polly](https://i.ibb.co/vJ9BmpC/polly.png")
![Swagger](https://img.shields.io/badge/-Swagger-%23Clojure?style=for-the-badge&logo=swagger&logoColor=white)

### Descrição do Projeto
O projeto Address API REST é uma aplicação que foi desenvolvida para demonstrar a utilização de retry no consumo de API e Cache em mémoria. 

### Construção da entidade:
-  Partindo do início, criei a entidade Address e, prevendo a necessidade de comparar dois objetos, adicionei a implementação da interface IEquatable que fornece os métodos Equals e GetHashCode para serem sobrepostos, de forma que o Equals, por exemplo, passe a comparar o conteúdo dos atributos da entidade, pois a forma normal é comprar o endereço de memória dos objetos. Também achei necessário criar um novo sumário para o método Equals para evitar enganos, visto que dei uma nova funcionalidade para o método, mas a descrição ainda continuava a mesma da funcionalidade anterior.
  
```csharp
 public sealed class Address : IEquatable<Address>
 {
     public Guid AddressId { get; set; }
     public string Street { get; set; }
     public string City { get; set; }
     public string State { get; set; }
     public string ZipCode { get; set; }
     public string Complements { get; set; }
     public string Neighborhood { get; set; }
     public string Ibge { get; set; }
     
     /// <summary>
     /// Determines whether the specified object is equal to the current object
     /// </summary>
     /// <param name="other"></param>
     /// <returns> <paramref name="true"/> if the specified atribures of object is equal to the current object; otherwise, false. </returns>
     public bool Equals(Address? other)
     {
         if (other is null) return false;
         if (ReferenceEquals(this, other)) return true;
         return Street == other.Street
             && City == other.City
             && State == other.State
             && ZipCode == other.ZipCode
             && Complements == other.Complements
             && Neighborhood == other.Ibge
             && Ibge == other.Ibge;
     }
     public override bool Equals(object obj) => Equals(obj as Address);
     public override int GetHashCode() => (Street, City, State, ZipCode, Complements,Neighborhood,Ibge).GetHashCode();

 }
  ```

> Sumário original
<img src="https://i.ibb.co/3rQkMHK/sumary-before.png" alt="sumary-before" border="0">

> Sumário sobreposto
<img src="https://i.ibb.co/5MqQG4G/sumary-after.png" alt="sumary-after" border="0">

### Versionamento da API
- Utilizei o pacote Asp.Versioning.Mvc.ApiExplorer para realizar o versionamento e configurei o Swagger para separar os end-points por versão. No botão saiba mais consta o artigo que li, para entender como realizar a configuração e como organizar o código. Nas imagens a baixo, mostra o projeto onde, na versão 1, constam os end-points padrão, e na versão 2 constam dois end-poins simulando que eles foram alterados e agora possuem mais campos.
  
> [Saiba mais](https://mohsen.es/api-versioning-and-swagger-in-asp-net-core-7-0-fe45f67d8419)

![V1](https://i.ibb.co/JvstDpj/address-swagger.png)  
![V2](https://i.ibb.co/fXMQNsP/address-swagger-v2.png) 

### Cache
- Escolhi Redis para o projeto, ele faz parte do tipo de fornecedores de cache distribuido. Uma das melhorias entregas no .Net 8 é poder configurar o Redis com poucas linhas de código. Utilizei o pacote Microsoft.Extensions.Caching.StackExchangeRedis 

- No arquivo **program.cs** configurei a injeção de dependencia.
```csharp
builder.Services.AddStackExchangeRedisOutputCache(

    options =>
    {
        options.Configuration = builder.Configuration.GetConnectionString("Redis");
        options.InstanceName = "intelligent_nightingale";
    });
```
- No controlador informei que o resultado do end-point fica 20 salvo em cache por 20 segundos.
```csharp
 [OutputCache(Duration = 20)]
 [HttpGet("GetById/{addressId:Guid}")]
 public async Task<IActionResult> GetById(Guid addressId)
 {
     await _addressS.GetByIdAsync(addressId);
     return Ok();
 }
```




