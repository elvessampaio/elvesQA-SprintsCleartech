using AutoMapper;
using Ecommerce_API.Data;
using Ecommerce_API.Data.DTOS.Produto;
using Ecommerce_API.Models;
using Ecommerce_API.Repository.Interfaces;
using Ecommerce_API.Services.Interfaces;

namespace Ecommerce_API.Services
{
    public class ProdutoService : IProdutoService
    {
        private EcommerceContext _context;
        private IMapper _mapper;
        private IProdutoRepository _repository;

        public ProdutoService(EcommerceContext context, IMapper mapper, IProdutoRepository repository)
        {
            _context = context;
            _mapper = mapper;
            _repository = repository;
        }

        private List<Produto> ListaProduto()
        {
            return _context.Produtos.ToList();
        }

     
        public async Task<Produto> CadastrarProduto(CreateProdutoDto produtoDto)
        {
            Produto produto = _mapper.Map<Produto>(produtoDto);
            VerificacaoDosDados(produtoDto.Nome, produtoDto.CategoriaId, produtoDto.SubcategoriaId, produtoDto.CentroDistribuicaoId);
            await _repository.CadastrarProduto(produtoDto);
            return produto;
        }

        public List<ReadProdutoDto> PesquisarProduto(FilterProdutoDto filtro)
        {
            var produto = _repository.PesquisarProduto(filtro);
            if (produto == null) return null;
            var produtoDto = new List<ReadProdutoDto>();
            _mapper.Map(produto, produtoDto);
            return produtoDto;
        }


        public ReadProdutoDto PesquisarProdutoId(int id)
        {
            var produto = _repository.PesquisarProdutoId(id);
            if (produto == null) return null;
 
            var produtoDto = _mapper.Map<ReadProdutoDto>(produto);
            return produtoDto;
        }

        public async Task<Produto> EditarProduto(UpdateProdutoDto produtoDto, int id)
        {
            Produto produto = _mapper.Map<Produto>(produtoDto);
            if (produto == null) return null;
            VerificacaoDosDados(produtoDto.Nome, produtoDto.CategoriaId, produtoDto.SubcategoriaId, produtoDto.CentroDistribuicaoId, id);
            produto = await _repository.EditarProduto(produtoDto, id);
            return produto;
        }

        public async Task<Produto> ApagarProduto(int id)
        {
            var produto = await _repository.ApagarProduto(id);
            if (produto == null) return null;
            return produto;
        }

        private void VerificacaoDosDados(string nome, int categoriaId, int subcategoriaId, int centroId, int? id = null)
        {
            if (ListaProduto().Any(c => c.Nome.ToUpper() == nome.ToUpper() && c.Id != id))
            {
                throw new NameExceptions("Já existe um produto com o mesmo nome.");
            }

            if (_context.Categorias.Any(c => c.Nome.ToUpper() == nome.ToUpper()))
            {
                throw new NameExceptions("Já existe uma Categoria com o mesmo nome.");
            }

            if (_context.SubCategorias.Any(c => c.Nome.ToUpper() == nome.ToUpper()))
            {
                throw new NameExceptions("Já existe uma Subcategoria com o mesmo nome.");
            }

            if (_context.CentrosDistribuicoes.Any(c => c.Nome.ToUpper() == nome.ToUpper()))
            {
                throw new NameExceptions("Já existe um Centro de Distribuição com o mesmo nome.");
            }

            if (_context.Categorias.Any(c => c.Id == categoriaId && c.Status == false ||
            _context.SubCategorias.Any(s => s.Id == subcategoriaId && s.Status == false || 
            _context.CentrosDistribuicoes.Any(c => c.Id == centroId && s.Status == false))))

            {
                throw new NameExceptions("A Categoria/Subcategoria/Centro de Distribuição está inativa, por este motivo, não é possível cadastrar/editar o produto.");
            }

        }
    }
    
}
