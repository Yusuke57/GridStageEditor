# GridStageEditor
UI Toolkitで作成したステージエディタです。
グリッド状にマスを配置するゲームのステージを作成できます。

<img width="695" alt="image" src="https://user-images.githubusercontent.com/27964732/208299570-3ffdddd8-70c3-4d8a-ba0c-0710c80374ad.png">

# 使い方
Unityのツールバーから `Window > GridStageEditorWindow` で開けます。

- 左側のステージIDをクリックすると、そのステージデータが右側に表示されます
- 上部のツールビューで種類を選択した上で、各マスのボタンをクリックすることで、マスを編集できます
- 「+」「-」ボタンで行や列を追加/削除できます
- ステージデータは `StageDataHolder` というScriptableObjectで管理しており、GridStageEditorWindowでの変更が反映されます

# 解説
こちらの記事で軽く解説しています。
https://zenn.dev/yuji_ap/articles/c18cde5449b886
