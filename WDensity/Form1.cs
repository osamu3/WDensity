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
		#region 広域変数宣言（描画オブジェクト関係）
		//オブジェクトの宣言のみ(実体はイニシャルメソッドの中で作成)
		private static Bitmap _CanvasFore;//(前景)描画先(canvas)とするビットマップオブジェクトの宣言
		private static Bitmap _CanvasBack;//(背景)描画先(canvas)とするビットマップオブジェクトの宣言
		private static Image _LoadedForeImgObj;//(前景)画像読込先のイメージオブジェクトの宣言
		private static Image _LoadedBackImgObj;//(背景)画像読込先のイメージオブジェクトの宣言
		#endregion

		#region 広域変数：ズーム変数
		public static float _ZoomRate = 1F;//倍率
		private static int _TrckBrZoomInitVal; //　　縮尺　　　　の初期値
		#endregion 
		//画像の描画領域(大きさ)と位置を保持する変数⇒領域を拡大・縮小することで画像を拡大・縮小できる。
//		private static Rectangle _CanvsRctForeImg = new Rectangle(0, 0, 0, 0);//(前景)画像用
		//		private static Rectangle _CanvsRctBackImg = new Rectangle(0, 0, 0, 0);//(背景)画像用変数

		#region 広域変数：描画位置変数
		private static float _ForePicDrwPsOfstX;//(前景)画像の中心＝(前景)描画キャンバスの中心となる位置へのオフセット
		private static float _ForePicDrwPsOfstY;
		private static float _BackPicDrwPsOfstX;//(背景)画像の中心＝(背景)描画キャンバスの中心となる位置へのオフセット
		private static float _BackPicDrwPsOfstY;
//kokokra		画像移動量積算
		#endregion

		#region 広域変数：画像サイズ
		private static int _ForePicInitSizemmmWidth;
		private static int _ForePicIniSizeLLLtHeigt;
		#endregion

		#region 広域変数：トラックバー変数
		private int _trckBrHrzMaxVal;  //(前景)移動用の水平移動トラックバーの最大値
		private int _trckBrVrtMaxVal;  //(前景)移動用の垂直移動トラックバーの最大値
		private int _trckBrHrzInitVal; //(前景)移動用の水平移動トラックバーの初期値
		private int _trckBrVrtInitVal; //(前景)移動用の垂直移動トラックバーの初期値

		
//		private int _trckBrHrzBackInitVal; //(背景)移動用の水平移動トラックバーの初期値
//		private int _trckBrVrtBackInitVal; //(背景)移動用の垂直移動トラックバーの初期値
//		private int _trckBrHrzBackMaxVal;  //(背景)移動用の水平移動トラックバーの最大値
		//		private int _trckBrVrtBackMaxVal;  //(背景)移動用の垂直移動トラックバーの最大値
		#endregion

		private DbgShowVal dbgShowVal;//デバッグ用のクラス

		public enum _EdtTyp : int {
			Horizontal = 1, Vertical = 2, Zoom = 3,Move = 4
		}

		#region 広域変数：マウスポインター変数
		private Point _picBxForeMouseDwnPt;
		private Boolean _picBxForeMouseDwnFlg = false;
		private Point _picBxBackMouseDwnPt;
		private Boolean _picBxBackMouseDwnFlg = false;
		#endregion

		public Form1() {
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e) {
			//デバッグ用
			dbgShowVal = new DbgShowVal(this);
			
			//			if (canvas != null) canvas.Dispose();

			//画像を読み込む
			_LoadedForeImgObj = Image.FromFile("Traget.jpg");
			_LoadedBackImgObj = Image.FromFile("House.jpg");

			//画像表示
			drawPic(picBxFore, _LoadedForeImgObj, ref _CanvasFore, "Fore");

			drawPic(picBxBack, _LoadedBackImgObj, ref _CanvasBack,"Back");
			_ForePicInitWidth = picBxFore.Width;
			_ForePicInitHeigt = picBxFore.Height;


			#region トラックバー値の初期設定
			//スライダーの【移動量の最大値】は、読込画像の大きさの１／２とする
			//(前景)処理用
			_trckBrHrzMaxVal = _LoadedForeImgObj.Width / 2; //水平移動トラックバーの最大値
			_trckBrHrzInitVal = _trckBrHrzMaxVal / 2;   //      〃    つまみの初期位置(読込画像の１／４)（水平移動用)

			_trckBrVrtMaxVal = _LoadedForeImgObj.Height / 2; //水平移動トラックバーの最大値（水平移動用)
			_trckBrVrtInitVal = _trckBrVrtMaxVal / 2;   //      〃    つまみの初期位置(読込画像の１／４)（水平移動用)

			trckBrHrz.Maximum = _trckBrHrzMaxVal;
			trckBrHrz.Value = _trckBrHrzInitVal;
			trckBrVrt.Maximum = _trckBrVrtMaxVal;
			trckBrVrt.Value = _trckBrVrtInitVal; 

#region  //背景
/*背景用
			_trckBrHrzBackMaxVal = _LoadedBackImgObj.Width / 2; //トラックバーの最大値（水平移動用)
			_trckBrVrtBackMaxVal = _LoadedBackImgObj.Height / 2;//　　　　　〃　　    （垂直移動用)
			_trckBrHrzBackInitVal = _trckBrHrzBackMaxVal / 2;   //つまみの初期位置(読込画像の１／４)（水平移動用)
			_trckBrVrtBackInitVal = _trckBrVrtBackMaxVal / 2;   //　　　　　〃　                  　（垂直移動用)

			 //(背景)画像の編集設定で起動
			//初期画面のトラックバーの位置を(背景)画像選択時の状態にする為、ラジオボタンを(背景)画像移動にしておく
			rdBtnBackPicMv.Checked = true;
			trckBrHrz.Maximum = _trckBrHrzBackMaxVal;
			trckBrVrt.Maximum = _trckBrVrtBackMaxVal;
			//スライダーのつまみの初期値設定
			trckBrHrz.Value = _trckBrHrzBackInitVal;
			trckBrVrt.Value = _trckBrVrtBackInitVal;
			//			_SclTrckBrInitVal = trckBrScl.Value;//縮尺トラックバーの初期位置を保存
			trckBrHrz.Size = new Size(picBxBack.Width, trckBrHrz.Size.Height);//幅をピクチャ―ボックスの幅とする
			trckBrVrt.Size = new Size(trckBrHrz.Size.Width, picBxBack.Height);//高さをピクチャ―ボックスの高さとする
*/			
#endregion
			#endregion

			_TrckBrZoomInitVal = trckBrZoom.Value;

			#region//デバッグ
			dbgShowVal.setVal(	backPicBxW:	picBxBack.Width,
								backPicBxH:	picBxBack.Height,
								loadImgBackW:_LoadedBackImgObj.Width,
								loadImgBackH:_LoadedBackImgObj.Height,
								canvsBackW:	_CanvasBack.Width,
								canvsBackH:	_CanvasBack.Height
			);
			#endregion
		
		}

		/// <summary>画像初期表示 </summary>
		/// <param name="picBox">画像表示先ピクチャーコントロール(カンバスの一部(全部)を表示)</param>
		/// <param name="loadedImg">参照呼出し　ファイルから読み込まれた画像</param>
		private void drawPic(PictureBox picBox, Image loadedImg, ref Bitmap canvas, string drawTarget) {
			//カンバス(描画先のビットマップオブジェクトの実体)を作成
			if (canvas != null)//古いカンバスがあれば廃棄
				canvas.Dispose();
			canvas = new Bitmap(loadedImg.Width, loadedImg.Height);

			//画像表示用カンバスと読み込み画像のDPIを同じにする。
			canvas.SetResolution(loadedImg.HorizontalResolution, loadedImg.VerticalResolution);

			Graphics g = Graphics.FromImage(canvas);
			//一旦画像クリア
			g.Clear(Color.Aqua);
			//変換行列初期化
			g.ResetTransform();
			#region //前景描画の場合
				if (drawTarget == "Fore") {
					_ForePicDrwPsOfstX = -loadedImg.Width / 2 + picBox.Width / 2;
					_ForePicDrwPsOfstY = -loadedImg.Height / 2 + picBox.Height / 2;
					//画像の中心がカンバスの中心に表示されるよう変換行列をセット
					g.TranslateTransform(_ForePicDrwPsOfstX, _ForePicDrwPsOfstY);
					g.DrawImage(loadedImg, 0,0);
					//PictureBox1に表示する
					picBox.Image = canvas;
				}
			#endregion

			#region //背景描画の場合
				if (drawTarget == "Back") {
					//ピクチャーボックスの大きさを読込画像の大きさとする
//					picBxBack.Size = loadedImg.Size;
					_BackPicDrwPsOfstX = -loadedImg.Width / 2 + picBox.Width / 2;
					_BackPicDrwPsOfstY = -loadedImg.Height / 2 + picBox.Height / 2;
					//画像の中心がカンバスの中心に表示されるよう変換行列をセット
//					g.TranslateTransform(_BackPicDrwPsOfstX, _BackPicDrwPsOfstY);
					g.DrawImage(loadedImg, 0, 0);
					//PictureBoxに表示する
					picBox.Image = canvas;
				}	
			#endregion
			g.Dispose();
		}

		/// <summary>ズームバーイベント⇒ 画像【縮尺】</summary>
		/// 
		private void ZoomBr_Scroll(object sender, EventArgs e) {
			//縮尺を計算
			_ZoomRate = 1.0F + ((float)(trckBrZoom.Value - _TrckBrZoomInitVal)) / 1000.0F;

			//縮尺に応じたカンバスを作成
			//旧カンバスをリセット
			if (_CanvasBack != null)	_CanvasBack.Dispose();
			_CanvasBack = new Bitmap((int)(_LoadedBackImgObj.Width*_ZoomRate), (int)(_LoadedBackImgObj.Height*_ZoomRate));
			//画像表示用カンバスと読み込み画像のDPIを同じにする。
			_CanvasBack.SetResolution(_LoadedBackImgObj.HorizontalResolution, _LoadedBackImgObj.VerticalResolution);


			Graphics g = Graphics.FromImage(_CanvasBack);
			//一旦画像クリア
			g.Clear(Color.Black);
	
			g.ResetTransform();	//ワールド変換行列を単位行列にリセット
			lbl_ZoomRate.Text = _ZoomRate.ToString("#.0000");
			g.ScaleTransform(_ZoomRate, _ZoomRate);
			g.TranslateTransform(_BackPicDrwPsOfstX, _BackPicDrwPsOfstY);
			g.DrawImage(_LoadedBackImgObj, 0, 0);

			//PictureBox1に表示する
			//縮尺に応じてピクチャーボックスを伸縮
//			picBxBack.Size = new Size((int)(_LoadedBackImgObj.Width * _ZoomRate), (int)(_LoadedBackImgObj.Height * _ZoomRate));
			picBxBack.Image = _CanvasBack;
			g.Dispose();
		}

		/// <summary>トラックバーイベント⇒ 画像【移動】</summary>
		/// 
		private void trckBr_Scroll(object sender, EventArgs e) {
			#region 画像【水平移動】トラックバーイベント
				if (((TrackBar)sender).Name.ToString() == "trckBrHrz") {
					//前景画像の移動
					movePicture(picBxFore,							//画像表示先ピクチャーボックスコントロール 
								_CanvasFore,						//描画先カンバス
								_LoadedForeImgObj,					//読み込んだ画像
								_EdtTyp.Horizontal,					//水平移動
								_ForePicDrwPsOfstX,					//画像中心表示のためのオフセット
								_ForePicDrwPsOfstY,					//              〃
								_trckBrHrzInitVal - trckBrHrz.Value,//スライダーの水平移動量
								0									//		〃    垂直移動量
							);
				}
			#endregion

			#region 画像【垂直移動】トラックバーイベント
			if (((TrackBar)sender).Name == "trckBrVrt") {
				//前景画像の移動
				movePicture(picBxFore,							//画像表示先ピクチャーボックスコントロール 
							_CanvasFore,						//描画先カンバス
							_LoadedForeImgObj,					//読み込んだ画像
							_EdtTyp.Vertical,					//水平移動
							_ForePicDrwPsOfstX,					//画像中心表示のためのオフセット
							_ForePicDrwPsOfstY,					//              〃
							0,									//スライダーの水平移動量
							_trckBrVrtInitVal - trckBrVrt.Value	//		〃    垂直移動量
						);
			}
			#endregion
		}

		/// <summary>画像【移動】 </summary>
		/// <param name="picBox">画像表示先ピクチャーコントロール(カンバスの一部(全部)を表示)</param>
		/// <param name="canvas">画像描画先キャンバス</param>
		/// <param name="loadedImg">ファイルから読み込まれた画像</param>
		/// <param name="edtTyp">水平or垂直移動</param>
		private void movePicture(PictureBox picBox, Bitmap canvas, Image loadedImg, _EdtTyp edtTyp,
								float ofSetX, float ofSetY, int mvHrzVal,int mvVrtVal) {
			Graphics g = Graphics.FromImage(canvas);

			g.Clear(Color.Black);//一旦画像クリア
			g.ResetTransform();//変換行列初期化

			#region 画像【水平移動】
			if (edtTyp == _EdtTyp.Horizontal) {
				//ピクチャボックスの中心＝カンバスの中心＋スライダー移動量　となるよう、変換行列をセット
				g.TranslateTransform(ofSetX+mvHrzVal, ofSetY+mvVrtVal);
				g.DrawImage(loadedImg,0,0);
				//PictureBox1に表示する
				picBox.Image = canvas;
				#region デバッグ出力
					#if DEBUG
						dbgShowVal.setVal(hrzBrForeMoveV: mvHrzVal);
					#endif
				#endregion
			}
			#endregion

			#region 画像【垂直移動】
			if (edtTyp == _EdtTyp.Vertical) {
				//ピクチャボックスの中心＝カンバスの中心＋スライダー移動量　となるよう、変換行列をセット
				g.TranslateTransform(ofSetX+mvHrzVal, ofSetY + mvVrtVal);
				g.DrawImage(loadedImg, 0, 0);//グラフィックオブジェクトに画像を描画
				//PictureBox1に表示する
				picBox.Image = canvas;
				#region デバッグ出力
					#if DEBUG
						dbgShowVal.setVal(vrtBrForeMoveV: mvVrtVal);
					#endif
				#endregion
			}
			#endregion
			#region 画像【上下左右移動】
			if (edtTyp == _EdtTyp.Move) {
//				こ０こから
				//ピクチャボックスの中心＝カンバスの中心＋スライダー移動量　となるよう、変換行列をセット
				g.TranslateTransform(ofSetX + mvHrzVal, ofSetY + mvVrtVal);
				g.DrawImage(loadedImg, 0, 0);//グラフィックオブジェクトに画像を描画
				//PictureBox1に表示する
				picBox.Image = canvas;
				#region デバッグ出力
#if DEBUG
				dbgShowVal.setVal(hrzBrBackMoveV: mvHrzVal,vrtBrBackMoveV: mvVrtVal);
#endif
				#endregion
			}
			#endregion

			g.Dispose();
		}

		private void button1_Click(object sender, EventArgs e) {
			Graphics g = Graphics.FromImage(_CanvasBack);

			//一旦画像クリア
			g.Clear(Color.Black);
			//変換行列初期化
			g.ResetTransform();

			//ピクチャボックスの中心＝カンバスの中心となるよう、変換行列をセット
//			g.TranslateTransform(-_BackPicDrwPsOfstX, -_BackPicDrwPsOfstY);
			g.DrawImage(_LoadedBackImgObj, new Rectangle(0, 0, _LoadedBackImgObj.Width, _LoadedBackImgObj.Height));

			//PictureBox1に表示する
			picBxBack.Image = _CanvasBack;
			#region デバッグ出力
				#if DEBUG
					dbgShowVal.setVal(backPicBxW: picBxBack.Width,
								backPicBxH: picBxBack.Height,
								loadImgBackW: _LoadedBackImgObj.Width,
								loadImgBackH: _LoadedBackImgObj.Height,
								canvsBackW: _CanvasBack.Width,
								canvsBackH: _CanvasBack.Height
					);
				#endif
			#endregion

			g.Dispose();
		}

		#region 前景画像マウスダウン
		private void picBxFore_MouseDown(object sender, MouseEventArgs e) {
			if ((e.Button & MouseButtons.Left) == MouseButtons.Left) {
				//位置を記憶する
				_picBxForeMouseDwnFlg = true;
				_picBxForeMouseDwnPt = new Point(e.X, e.Y);
			}
		} 
		#endregion

		#region 前景画像マウスムーブ
		private void picBxFore_MouseMove(object sender, MouseEventArgs e) {
			if (_picBxForeMouseDwnFlg)
				picBxFore.Location = new Point(picBxFore.Location.X + e.X - _picBxForeMouseDwnPt.X,
											picBxFore.Location.Y + e.Y - _picBxForeMouseDwnPt.Y);
		}
		
		#endregion

		#region 前景画像マウスアップ
		private void picBxFore_MouseUp(object sender, MouseEventArgs e) {
			_picBxForeMouseDwnFlg = false;
		}
		
		#endregion

		private void numericUpDown1_ValueChanged(object sender, EventArgs e) {
			picBxFore.Width = _ForePicInitWidth + (int)((NumericUpDown)sender).Value;
		}

		private void numericUpDown2_ValueChanged(object sender, EventArgs e) {
			picBxFore.Height = _ForePicInitHeigt + (int)((NumericUpDown)sender).Value;
		}

		/// <summary>背景画像：マウスダウン </summary>
		///
		private void picBxBack_MouseDown(object sender, MouseEventArgs e) {
			if ((e.Button & MouseButtons.Left) == MouseButtons.Left) {
				//位置を記憶する
				_picBxBackMouseDwnFlg = true;
				_picBxBackMouseDwnPt = new Point(e.X, e.Y);
			}
		}

		/// <summary>背景画像：マウスムーブ </summary>
		///
		private void picBxBack_MouseMove(object sender, MouseEventArgs e) {
			if (_picBxBackMouseDwnFlg)
				//背景画像の移動
				movePicture(picBxBack,							//背景画像ピクチャーボックス 
					_CanvasBack,						//描画先カンバス
					_LoadedBackImgObj,					//読み込んだ画像
					_EdtTyp.Move,					//編集タイプ：画像移動
					_BackPicDrwPsOfstX,				//画像中心表示のためのオフセット
					_BackPicDrwPsOfstY,					//              〃
					e.X - _picBxBackMouseDwnPt.X,	//画像水平移動量
					e.Y - _picBxBackMouseDwnPt.Y	//画像水平移動量
				);
		}

		/// <summary>背景画像：マウスアップ </summary>
		///
		private void picBxBack_MouseUp(object sender, MouseEventArgs e) {
			_picBxBackMouseDwnFlg = false;
		} 
	}
}