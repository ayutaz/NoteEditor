using NoteEditor.Model;
using UniRx;

namespace NoteEditor.Presenter
{
    public class ModelSizeSpinBoxPresenter : SpinBoxPresenterBase
    {
        protected override ReactiveProperty<int> GetReactiveProperty()
        {
            return EditData.ModelSize;
        }
    }
}