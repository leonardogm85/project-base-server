using ProjetoBase.Application.ViewModels;
using ProjetoBase.Infrastructure.CrossCutting.Common.Tables;

namespace ProjetoBase.Application.Interfaces
{
    public interface ITabelaAppService<TTableViewModel>
        where TTableViewModel : ViewModel
    {
        TableResult<TTableViewModel> ObterTabela(TableFilter filtro);
    }
}
