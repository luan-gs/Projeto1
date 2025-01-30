using ApiCrud.Data;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace ApiCrud.Estudantes;

public static class EstudantesRotas 
{
    public static void AddRotasEstudantes(this WebApplication app)
    {
        RouteGroupBuilder rotasEstudantes = app.MapGroup("estudantes");
        
        rotasEstudantes.MapPost("", async (AddEstudanteRequest request, AppDbContext context) => 
        {
            try
            {
                Estudante? cpfExistente = await context.Estudantes.Find(e => e.CPF == request.CPF).FirstOrDefaultAsync();
                if (cpfExistente != null)
                    return Results.Conflict("O CPF informado já está cadastrado");
                
                Estudante novoEstudante = new(request.Nome);
                await context.Estudantes.InsertOneAsync(novoEstudante);
                return Results.Created($"/estudantes/{novoEstudante.Id}", novoEstudante);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        });

        rotasEstudantes.MapGet("", async (Guid id, AppDbContext context) => 
        {
            Estudante? estudante = await context.Estudantes.Find(e => e.Id == id && e.Ativo).FirstOrDefaultAsync();
            
            if (estudante == null)
                return Results.NotFound();

            EstudanteDto estudanteDto = new(estudante.Id, estudante.Nome, estudante.CPF);
            return Results.Ok(estudanteDto);
        });

        rotasEstudantes.MapPut("{id}", async (Guid id, UpdateEstudanteRequest request, AppDbContext context) => 
        {
            var filter = Builders<Estudante>.Filter.Eq(e => e.Id, id);
            var update = Builders<Estudante>.Update.Set(e => e.Nome, request.Nome);

            var estudante = await context.Estudantes.Find(e => e.Id == id).FirstOrDefaultAsync();

            if (estudante == null)
                return Results.NotFound();

            await context.Estudantes.UpdateOneAsync(filter, update);
            
            return Results.Ok();
        });

        rotasEstudantes.MapDelete("{id}", async (Guid id, AppDbContext context) => 
        {
            var filter = Builders<Estudante>.Filter.Eq(e => e.Id, id);
            var update = Builders<Estudante>.Update.Set(e => e.Ativo, false);

            var estudante = await context.Estudantes.Find(e => e.Id == id).FirstOrDefaultAsync();

            if (estudante == null)
                return Results.NotFound();

            await context.Estudantes.UpdateOneAsync(filter, update);

            return Results.Ok();
        });
    }
}