using AutoMapper;
using Ecommerce_API.Data;
using Ecommerce_API.Data.DTOS.SubCategoria;
using Ecommerce_API.Models;
using Ecommerce_API.Repository.Interfaces;
using Ecommerce_API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_API.Services
{
    public class SubcategoriaService : ISubcategoriaService
    {
        private EcommerceContext _context;
        private IMapper _mapper;
        private ISubcategoriaRepository _repository;
        public SubcategoriaService(EcommerceContext context, IMapper mapper, ISubcategoriaRepository repository)
        {
            _context = context;
            _mapper = mapper;
            _repository = repository;
        }
        private List<SubCategoria> ListaSubcategoria()
        {
            return _context.SubCategorias.ToList();
        }

        public async Task<SubCategoria> CadastrarSubCategoria(CreateSubCategoriaDto subCategoriaDto)
        {
            SubCategoria subCategoria = _mapper.Map<SubCategoria>(subCategoriaDto);
            VerificacaoDosDados(subCategoriaDto.Nome, subCategoria.CategoriaId);
            await _repository.CadastrarSubcategoria(subCategoriaDto);
            return subCategoria;
        }

        public List<ReadSubCategoriaDto> PesquisarSubcategoria(FilterSubCategoriaDto filtro)
        {
            var subcategoria = _repository.PesquisarSubcategoria(filtro);
            if (subcategoria == null) return null;
            var subcategoriaDto = new List<ReadSubCategoriaDto>();
            _mapper.Map(subcategoria, subcategoriaDto);

             return subcategoriaDto;

        }

        public ReadSubCategoriaDto PesquisarSubCategoriaId(int id)
        {
            var subCategoria = _repository.PesquisarSubcategoriaId(id);
            if (subCategoria == null) return null;
            var subCategoriaDto = _mapper.Map<ReadSubCategoriaDto>(subCategoria);
            return subCategoriaDto;
        }

        public async Task<SubCategoria> EditarSubCategoria([FromBody] UpdateSubCategoriaDto subCategoriaDto, int id)
        {
            SubCategoria subcategoria = _mapper.Map<SubCategoria>(subCategoriaDto);
            if (subcategoria == null) return null;
            VerificacaoDosDados(subCategoriaDto.Nome, subcategoria.CategoriaId, subcategoria.Status, id);
            subcategoria = await _repository.EditarSubcategoria(subCategoriaDto, id);

            return subcategoria;
        }

       
        public async Task<SubCategoria> ApagarSubCategoria(int id)
        {
            var subCategoria = await _repository.ApagarSubcategoria(id);
            if (subCategoria == null) return null;
            return subCategoria;

        }

        private void VerificacaoDosDados(string nome, int categoriaId, bool? status = null, int? id = null)
        {
            if (ListaSubcategoria().Any(c => c.Nome.ToUpper() == nome.ToUpper() && c.Id != id))
            {
                throw new NameExceptions("Já existe uma SubCategoria com o mesmo nome.");
            }

            if (_context.Categorias.Any(c => c.Nome.ToUpper() == nome.ToUpper()))
            {
                throw new NameExceptions("Já existe uma Categoria com o mesmo nome.");
            }

            if (_context.Categorias.Any(c => c.Id == categoriaId && c.Status == false))
            {
                throw new NameExceptions("A Categoria está inativa, por este motivo, não é possível editar a Subcategoria.");
            }
            if (_context.Produtos.Any(c => c.Nome.ToUpper() == nome.ToUpper()))
            {
                throw new NameExceptions("Já existe um Produto com o mesmo nome.");
            }

            if (_context.CentrosDistribuicoes.Any(c => c.Nome.ToUpper() == nome.ToUpper()))
            {
                throw new NameExceptions("Já existe um Centro de Distribuição com o mesmo nome.");
            }

            if (_context.Produtos.Where(c => c.SubcategoriaId == id).Count() > 0 && status == false)
            {
                throw new NameExceptions("Há produtos ativos nesta Subcategoria, por este motivo, não é possível inativá-la.");
            }

            
        }

    }

}
