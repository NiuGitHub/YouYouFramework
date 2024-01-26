using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDotweenAsync : MonoBehaviour
{
    void Start()
    {
        TestAsync().Forget();
    }

    private async UniTaskVoid TestAsync()
    {
        // ����ִ��
        await transform.DOMove(transform.position + Vector3.up, 1.0f);
        await transform.DOScale(Vector3.one * 2.0f, 1.0f);

        // UniTask.WhenAllͬʱ���в��ȴ���ֹ
        await
        (
            transform.DOMove(Vector3.zero, 1.0f).ToUniTask(),
            transform.DOScale(Vector3.one, 1.0f).ToUniTask()
        );

        //����Cancellation
        var ct = this.GetCancellationTokenOnDestroy();
        await UniTask.WhenAll(
            transform.DOMoveX(10, 3).WithCancellation(ct),
            transform.DOScale(10, 3).WithCancellation(ct));
    }
}
