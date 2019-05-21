using CRUDApp.Data;
using CRUDApp.ViewModels;

namespace CRUDApp.Extensions
{
    public static class ModelsConverterExtensions
    {
        public static Note ToModel(this NoteViewModel viewModel)
        {
            return new Note
            {
                Id = viewModel.Id,
                Description = viewModel.Description
            };
        }

        public static NoteViewModel ToViewModel(this Note model)
        {
            return new NoteViewModel
            {
                Id = model.Id,
                Description = model.Description
            };
        }
    }
}
