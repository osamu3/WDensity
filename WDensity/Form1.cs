using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WDensity {
/// <summary>
/// ファイルの画像は、イメージオブジェクトに読み込まれ、
/// ビットマップオブジェクト型のキャンバスに描画され
/// キャンバスのグラフィックオブジェクトのワールド座標変換メソッドにより移動／拡大・縮小／変形され
/// 画面表示は、ピクチャーボックスコントロールで行なわれる。
/// 
/// </summary>


	public partial class Form1 : Form {
		//オブジェクトの宣言のみ(実体はイニシャルメソッドの中で作成)
		private static Bitmap _BmpCanvasBck;//背景描画先(canvas)とするビットマップオブジェクトの宣言
		private static Bitmap _BmpCanvasFrt;//前景描画先(canvas)とするビットマップオブジェクトの宣言
		private static Image _LoadedBckImgObj;//読み込んだ背景画像のイメージオブジェクトの宣言
		private static Image _LoadedFrtImgObj;//読み込んだ前景描画のイメージオブジェクトの宣言


		public static float _ZoomRatio = 1F;//倍率
		//画像の描画領域(大きさ)と位置を保持する変数⇒領域を拡大・縮小することで画像を拡大・縮小できる。
		private static Rectangle _RctBckImg = new Rectangle(0, 0, 0, 0);//背景画像用変数
		private static Rectangle _RctFrtImg;//前景画像用

		private static int _BckPicDrwPsOfstX;//背景画像の中心＝背景描画キャンバスの中心となる位置へのオフセット
		private static int _BckPicDrwPsOfstY;
		private static int _FrtPicDrwPsOfstX;//前景画像の中心＝前景描画キャンバスの中心となる位置へのオフセット
		private static int _FrtPicDrwPsOfstY;

		private int _SclTrckBrInitialVal; //　　縮尺　　　　の初期値
		private int _trckBrHrzInitialVal; //水平移動トラックバーの初期値
		private int _trckBrVrtInitialVal; //垂直移動トラックバーの初期値

		public enum _EdtTyp : int {
			Horizontal = 1, Vertical = 2, Scale = 3
		}

		public Form1() {
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e) {
			DebugShowValue debugShowValue = new DebugShowValue(this);
			debugShowValue.aa();
	
			
			//			if (canvas != null) canvas.Dispose();

			//描画先(canvas)のビットマップオブジェクトの実体を作成
			_BmpCanvasBck = new Bitmap(picBxBck.Width, picBxBck.Height);
			_BmpCanvasFrt = new Bitmap(picBxFrt.Width, picBxFrt.Height);
			//宣言した描画先(canvas)のGraphicsオブジェクトの実態を作成
//			_G_CanvasBck = Graphics.FromImage(_CanvasBck);
//			_G_CanvasFrt = Graphics.FromImage(_CanvasFrt);

			//背景画像を読み込む
			_LoadedBckImgObj = Image.FromFile("house.jpg");
			_LoadedFrtImgObj = Image.FromFile("traget.jpg");

			//カンバスの大きさをセットする為の読込画像の大きさ⇒∴ピクチャーボックスの大きさ！＝画像の大きさ
			_RctBckImg = new Rectangle(0, 0, _LoadedBckImgObj.Width, _LoadedBckImgObj.Height);
			_RctFrtImg = new Rectangle(0, 0, _LoadedFrtImgObj.Width, _LoadedFrtImgObj.Height);


			_BckPicDrwPsOfstX = (_LoadedBckImgObj.Width - picBxBck.Width) / 2;
			_BckPicDrwPsOfstY = (_LoadedBckImgObj.Height - picBxBck.Height) / 2;
			_FrtPicDrwPsOfstX = (_LoadedFrtImgObj.Width - picBxFrt.Width) / 2;
			_FrtPicDrwPsOfstY = (_LoadedFrtImgObj.Height - picBxFrt.Height) / 2;
			//			zoomRatio = 1d;

			#region　【画像の初期表示】読み込んだ画像の中心が、カンバスの中心になるよう描画
			//　　　       〃　　　　　　　　　　前景画像　　　　　　〃
			drawPic2Center(picBxFrt, _BmpCanvasFrt, _FrtPicDrwPsOfstX, _FrtPicDrwPsOfstY, _LoadedFrtImgObj, _RctFrtImg);
			//　　　       〃　　　　　　　　　　背景画像　　　　　　〃
			drawPic2Center(picBxBck, _BmpCanvasBck, _BckPicDrwPsOfstX, _BckPicDrwPsOfstY, _LoadedBckImgObj, _RctBckImg);
			#endregion

			#region トラックバー値の初期設定
			//スライダーの【移動量の最大値】は、背景画像の大きさの１／２とする(±１／１)
			trckBrHrz.Maximum = _LoadedBckImgObj.Width / 2;
			trckBrVrt.Maximum = _LoadedBckImgObj.Height / 2;
			trckBrHrz.Size = new Size(picBxBck.Width, trckBrHrz.Size.Height);
			trckBrVrt.Size = new Size(trckBrHrz.Size.Width, picBxBck.Height);
			
			//スライダーの【つまみの初期値】は背景画像の大きさの１／４とする（移動量は±１／２）
			trckBrHrz.Value = _LoadedBckImgObj.Width / 4;
			trckBrVrt.Value = trckBrVrt.Maximum / 2;
			_SclTrckBrInitialVal = trckBrScl.Value;//縮尺トラックバーの初期位置を保存
			_trckBrHrzInitialVal = trckBrHrz.Value;//水平トラックバーの初期位置を保存
			_trckBrVrtInitialVal = trckBrVrt.Value;//垂直トラックバーの初期位置を保存

			#endregion
		}


		/// <summary>画像初期表示：読み込んだ画像の中心が、ピクチャーボックスの中心となる位置に表示
		/// </summary>
		/// <param name="picBox">画像表示先ピクチャーコントロール(カンバスの一部(全部)を表示)</param>
		/// <param name="canvas">画像描画先キャンバス</param>
		/// <param name="ofstX"></param>
		/// <param name="ofstY"></param>
		/// <param name="loadedImg">ファイルから読み込まれた画像</param>
		/// <param name="rct"></param>
		private void drawPic2Center(PictureBox picBox,Bitmap canvas, int ofstX, int ofstY, Image loadedImg, Rectangle rct) {
			Graphics g = Graphics.FromImage(canvas);
			//一旦画像クリア
			g.Clear(Color.Black);
			g.ResetTransform();
			//カンバスの中心がピクチャーボックス中止になるようオフセット分移動、カンバスのワールド変換行列を水平移動
			g.TranslateTransform(-ofstX, -ofstY);
			g.DrawImage(loadedImg, rct);

			//PictureBoxに表示する
			picBox.Image = canvas;

			g.Dispose();
		}

		/// <summary>トラックバーイベント⇒ 画像【移動】【縮尺】
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void trckBr_Scroll(object sender, EventArgs e) {
label2.Text = "水平：現在値＝" + trckBrHrz.Value.ToString();
label3.Text = "水平：初期値＝" + _trckBrHrzInitialVal.ToString();
label4.Text = "水平：移動値＝" + (_trckBrHrzInitialVal-trckBrHrz.Value).ToString();
			#region 画像【水平移動】トラックバーイベント
			if (((TrackBar)sender).Name == "trckBrHrz") {
				//前景？
				if (rdBtnFrtPicMv.Checked) {
					movePicture(picBxFrt,							//画像表示先ピクチャーボックスコントロール 
								_BmpCanvasFrt,						//描画先カンバス
								_LoadedFrtImgObj,					//描画先カンバス
								_EdtTyp.Horizontal,					//読み込んだ画像
								_trckBrHrzInitialVal - trckBrHrz.Value,	//スライダーの移動量
								_RctFrtImg								//読込画像の大きさ＝カンバスの大きさ
								);
				}
				//背景？
				if (rdBtnBckPicMv.Checked) {
					movePicture(picBxBck, _BmpCanvasBck, _LoadedBckImgObj,_EdtTyp.Horizontal,
								_trckBrHrzInitialVal - trckBrHrz.Value, _RctBckImg);
				}
			}
			#endregion

			#region 画像【垂直移動】トラックバーイベント
			if (((TrackBar)sender).Name == "trckBrVrt") {
				//前景？
				if (rdBtnFrtPicMv.Checked) {
					movePicture(picBxFrt, _BmpCanvasFrt, _LoadedFrtImgObj, _EdtTyp.Vertical,
								_trckBrVrtInitialVal - trckBrVrt.Value, _RctFrtImg);
				}
				//背景？
				if (rdBtnBckPicMv.Checked) {
					movePicture(picBxBck, _BmpCanvasBck, _LoadedBckImgObj, _EdtTyp.Vertical,
								_trckBrVrtInitialVal - trckBrVrt.Value, _RctBckImg);
				}
			}
			#endregion

			#region 画像【縮尺】トラックバーイベント
			if (((TrackBar)sender).Name == "trckBrScl") {
				//前景？
				if (rdBtnFrtPicMv.Checked) {
					movePicture(picBxFrt, _BmpCanvasFrt, _LoadedFrtImgObj, _EdtTyp.Scale,
								trckBrScl.Value - _SclTrckBrInitialVal, _RctFrtImg);
				}
				//背景？
				if (rdBtnBckPicMv.Checked) {
					movePicture(picBxBck, _BmpCanvasBck, _LoadedBckImgObj, _EdtTyp.Scale,
								trckBrScl.Value - _SclTrckBrInitialVal, _RctBckImg);
				}
			}
			#endregion
		}


		/// <summary>画像【移動】【縮尺処理】
		/// 
		/// </summary>
		/// <param name="picBox">画像表示先ピクチャーコントロール(カンバスの一部(全部)を表示)</param>
		/// <param name="canvas">画像描画先キャンバス</param>
		/// <param name="loadedImg">ファイルから読み込まれた画像</param>
		/// <param name="edtTyp">水平or垂直移動</param>
		private void movePicture(PictureBox picBox, Bitmap canvas, Image loadedImg, _EdtTyp edtTyp, int mvVal,Rectangle canvasRect) {
			Graphics g = Graphics.FromImage(canvas);
			//一旦画像クリア
			g.Clear(Color.Black);

			#region 画像【水平移動】
			if (edtTyp == _EdtTyp.Horizontal) {
				//変換行列初期化
				g.ResetTransform();
label1.Text = "水平移動量＝"+mvVal.ToString();
				//ピクチャボックスの中心＝カンバスの中心＋スライダー移動量　となるよう、変換行列をセット
				g.TranslateTransform(-_BckPicDrwPsOfstX - mvVal, -_BckPicDrwPsOfstY);
				//グラフィックオブジェクトに背景画像を背景画像の大きさで描画する。
				//-----------------------------------------------------------------------------------------------------
				//ＩＮＧ：_imgPicBack.Width/Heightではなく、_スケール.Width/Heightを用いる必要がある ↓
				//				           この場合だと、画像そのものの大きさ＝縮尺０↓             ↓
				//-----------------------------------------------------------------------------------------------------
				g.DrawImage(loadedImg, canvasRect);
				//PictureBox1に表示する
				picBox.Image = canvas;
			}
			#endregion

			#region 画像【垂直移動】
			if (edtTyp == _EdtTyp.Vertical) {
				g.ResetTransform();
label1.Text = "垂直移動量＝" + mvVal.ToString();
				//ピクチャボックスの中心＝カンバスの中心＋スライダー移動量　となるよう、変換行列をセット
				g.TranslateTransform(-_BckPicDrwPsOfstX, -_BckPicDrwPsOfstY + mvVal);
				g.DrawImage(loadedImg, canvasRect);

				//PictureBox1に表示する
				picBox.Image = canvas;
			}
			#endregion

			#region 画像【縮尺】
			float scaleVal = 0.0F;
			if (edtTyp == _EdtTyp.Scale) {
				g.ResetTransform();	//ワールド変換行列を単位行列にリセット
				if ((trckBrScl.Value - _SclTrckBrInitialVal) >= 0) {//スライダーが中央より右にスライドした場合
					scaleVal = 1F + ((float)(trckBrScl.Value - _SclTrckBrInitialVal)) / 100F;
					label1.Text = scaleVal.ToString();
					g.ScaleTransform(scaleVal, scaleVal);
				}
				g.DrawImage(loadedImg, canvasRect);
				//PictureBox1に表示する
				picBox.Image = _BmpCanvasBck;
			}
			#endregion

			g.Dispose();
		}

		private void button1_Click(object sender, EventArgs e) {
			Graphics g = Graphics.FromImage(_BmpCanvasBck);

			//一旦画像クリア
			g.Clear(Color.Black);
			//変換行列初期化
			g.ResetTransform();

label1.Text = _BckPicDrwPsOfstX.ToString();
			//ピクチャボックスの中心＝カンバスの中心となるよう、変換行列をセット
			g.TranslateTransform(-_BckPicDrwPsOfstX, -_BckPicDrwPsOfstY);
			g.DrawImage(_LoadedBckImgObj, new Rectangle(0, 0, _LoadedBckImgObj.Width, _LoadedBckImgObj.Height));

			//PictureBox1に表示する
			picBxBck.Image = _BmpCanvasBck;

			g.Dispose();
		}

		private void areat(int frtImgW, int frtImgH,int bckImgW, int bckImgH,int trbVMx,int trbHMx,int trbrVV,int trbrHV) {
			label5.Text = "背景画像の幅：" + _RctBckImg.Width.ToString();
			label6.Text = "水平トラックバーの現在値：" + trckBrHrz.Value.ToString();

		}

	}
}