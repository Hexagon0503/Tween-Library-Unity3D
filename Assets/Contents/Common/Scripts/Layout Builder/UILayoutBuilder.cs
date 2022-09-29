using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;

public class UILayoutBuilder : ILayoutBuilder
{
	[SerializeField] private MonoBehaviour[] layoutGroup;

	/// <summary>
	/// 
	/// </summary>
	public override void Build()
	{
		BuildLayout().Forget();
	}

	/// <summary>
	/// 
	/// </summary>
	/// <returns></returns>
	async UniTask BuildLayout()
    {
		SetGroupActive(true);
		await UniTask.Delay(1500, true, PlayerLoopTiming.Update, cancellationToken: this.GetCancellationTokenOnDestroy());
		LayoutRebuilder.ForceRebuildLayoutImmediate(_rectTransform);
		SetGroupActive(false);
	}

    /// <summary>
    /// 
    /// </summary>
    /// <param name="active"></param>
    void SetGroupActive(bool active)
	{
		for (int i = 0; i < layoutGroup.Length; i++)
		{
			layoutGroup[i].enabled = active;
		}
	}
}
