# MaterialBuilder

SubstancePainter で出力した Texture を元に Material を構築する便利ツールです。  
VRChatのワールド作成時などにご利用ください。

## 導入方法

VCCをインストール済みの場合、以下の**どちらか一つ**の手順を行うことでインポートできます。

- [https://tp-jp.github.io/vpm-repos/](https://tp-jp.github.io/vpm-repos/) へアクセスし、「Add to VCC」をクリック

- VCCのウィンドウで `Setting - Packages - Add Repository` の順に開き、 `https://tp-jp.github.io/vpm-repos/index.json` を追加

[VPM CLI](https://vcc.docs.vrchat.com/vpm/cli/) を使用してインストールする場合、コマンドラインを開き以下のコマンドを入力してください。

```
vpm add repo https://tp-jp.github.io/vpm-repos/index.json
```

VCCから任意のプロジェクトを選択し、「Manage Project」から「Manage Packages」を開きます。
一覧の中から `MaterialBuilder` の右にある「＋」ボタンをクリックするか「Installed Vection」から任意のバージョンを選択することで、プロジェクトにインポートします。
![image](https://github.com/user-attachments/assets/1aed6aed-c56d-43a8-8e92-4100583ba43f)

リポジトリを使わずに導入したい場合は [releases](https://github.com/tp-jp/material-builder/releases) から unitypackage をダウンロードして、プロジェクトにインポートしてください。


## Install manually (UPM)

以下を UPM でインストールしてください。
```
https://github.com/tp-jp/material-builder.git?path=Packages/com.tp-lab.material-builder
```

## 使い方

1. ツールバーから `TpLab>MaterialBuilder` を選択します。

2. 表示されたウィンドウの設定を行い、Material構築を実施します。

   - Textureが格納されている入力フォルダ     
     Textureが格納されているフォルダを指定します。
   
   - Materialを出力する出力フォルダ     
     Materialを出力したいフォルダを指定します。
   
   - 出力するMaterialのShader     
     出力するMaterialのShaderを指定します。
     - Autodesk Interactive
     - Standard
   
   - NormalMapのTextureTypeを自動で設定する  
     NormalMap の TextureType が NormalMap 以外の場合、 構築時に自動で  
     TextureType を NormalMap に設定する機能です。
   
   - Build  
     Material の構築を開始します。

## 更新履歴

[CHANGELOG](CHANGELOG.md)
