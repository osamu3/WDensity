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
    public partial class Form1 : Form {
        //オブジェクトの宣言のみ(実体はイニシャルメソッドの中で作成)
        private Bitmap _canvasBck;//背景描画先(canvas)とするImageオブジェクト(ビットマップオブジェクト)の宣言
		private Bitmap _canvasFrt;//前景描画先(canvas)とするImageオブジェクト(ビットマップオブジェクト)の宣言
		private Graphics _gCanvasBck;//背景描画先(canvas)のGraphicsオブジェクトの宣言
		private Graphics _gCanvasFrt;//前景描画先(canvas)のGraphicsオブジェクトの宣言
		private Image _imgObjBck;//背景を描画するイメージオブジェクトの宣言
		private Image _imgObjFrt;//前景を描画するイメージオブジェクトの宣言


        private double zoomRatio = 1d;//倍率
        //画像の描画領域(大きさ)と位置を保持する変数⇒領域を拡大・縮小することで画像を拡大・縮小できる。
        private Rectangle _rctBckImg = new Rectangle(0, 0, 0, 0);//背景画像用変数
        private Rectangle _rctFrtImg;//前景画像用

        private int _storeHrzSldrVal; //水平スライダー移動量計算用スライダー値一時保存用(前回の値－今回の値で計算する)
        private int _storeVrtSldrVal; //垂直スライダー移動量計算用スライダー値一時保存用(前回の値－今回の値で計算する)

        public enum _EdtTyp : int {
            Horizontal = 1,Vertical = 2,Scale = 3
        }        
        
        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
//			if (canvas != null) canvas.Dispose();

            //宣言した描画先(canvas)とするImageオブジェクト(ビットマップオブジェクト)の実体を作成
            _canvasBck = new Bitmap(picBxBck.Width, picBxBck.Height);
			_canvasFrt = new Bitmap(picBxFrt.Width, picBxFrt.Height);
			//宣言した描画先(canvas)のGraphicsオブジェクトの実態を作成
            _gCanvasBck = Graphics.FromImage(_canvasBck);
			_gCanvasFrt = Graphics.FromImage(_canvasFrt);

            //背景画像を読み込む
            _imgObjBck = Image.FromFile("house.jpg");
			_imgObjFrt = Image.FromFile("traget.jpg");

			//背景描画領域を読込背景画像の大きさにセット
			_rctBckImg = new Rectangle(0, 0, _imgObjBck.Width, _imgObjBck.Height);
			_rctFrtImg = new Rectangle(0, 0, _imgObjFrt.Width, _imgObjFrt.Height);

			//			zoomRatio = 1d;
			//読み込んだ背景画像の中心が、キャンバスの中心になるようにワールド座標を使って平行移動する。
			//ワールド変換行列を単位行列にリセット
			_gCanvasBck.ResetTransform();
			_gCanvasBck.TranslateTransform(-(_imgObjBck.Width - picBxBck.Width) / 2, -(_imgObjBck.Height - picBxBck.Height) / 2);
			//画像を描画
			_gCanvasBck.DrawImage(_imgObjBck, _rctBckImg);
			//PictureBox1に表示する
			picBxBck.Image = _canvasBck;

			//同様に、前景画像についてワールド座標を使って平行移動する。
			_gCanvasFrt.ResetTransform();
			_gCanvasFrt.TranslateTransform(-(_imgObjFrt.Width - picBxFrt.Width) / 2, -(_imgObjFrt.Height - picBxFrt.Height) / 2);
			//画像を描画
			_gCanvasFrt.DrawImage(_imgObjFrt, _rctFrtImg);
			//PictureBox1に表示する
			picBxFrt.Image = _canvasFrt;


			//トラックバーのスライダー移動量の最大値は、背景画像の大きさの１／２とする(±１／１)
            trckBrHrz.Maximum = _imgObjBck.Width / 2;
            trckBrVrt.Maximum = _imgObjBck.Height / 2;
			trckBrHrz.Size = new Size(picBxBck.Width,trckBrHrz.Size.Height);
			trckBrVrt.Size = new Size(trckBrHrz.Size.Width, picBxBck.Height);

            //トラックバーのスライダーのつまみの初期値は背景画像の大きさの１／４とする（移動量は±１／２）
            trckBrHrz.Value = _imgObjBck.Width / 4;
            trckBrVrt.Value = trckBrVrt.Maximum / 2;
            _storeHrzSldrVal = trckBrHrz.Value;//水平スライダーのつまみの位置を保存
            _storeVrtSldrVal = trckBrVrt.Value;//垂直スライダーのつまみの位置を保存
		}

		private void trckBrHrz_Scroll(object sender, EventArgs e) {
			//背景移動ラジオボタンのオンの場合
			if (rdBtnBckPicMv.Checked) {
				//一旦画像クリア
				_gCanvasBck.Clear(Color.Black);
				//水平スライダーの初期値からの移動量分、画像のワールド変換行列を水平移動
				_gCanvasBck.TranslateTransform(-(trckBrHrz.Value - _storeHrzSldrVal), 0);
				_storeHrzSldrVal = trckBrHrz.Value;//水平スライダーの現在値を保存
				_gCanvasBck.DrawImage(_imgObjBck, new Rectangle(0, 0, _imgObjBck.Width, _imgObjBck.Height));

			//PictureBox1に表示する
				picBxBck.Image = _canvasBck;
			}
		}

		private void trckBr_Scroll(object sender, EventArgs e) {
			//画像移動のラジオボタンがチェックされ
			if (rdBtnBckPicMv.Checked){
					movePicture(picBxBck, _gCanvasBck, _EdtTyp.Horizontal);
			}
			if (rdBtnFrtPicMv.Checked){
					movePicture(picBxFrt, _gCanvasFrt, _EdtTyp.Vertical);
			}
		}



		private void movePicture(PictureBox picBox, Graphics g ,_EdtTyp edtTyp) {
			//水平移動の場合
			if (edtTyp == _EdtTyp.Horizontal) {
				//一旦画像クリア
				g.Clear(Color.Black);
				//水平スライダーの初期値からの移動量分、画像のワールド変換行列を水平移動
				g.TranslateTransform(-(trckBrHrz.Value - _storeHrzSldrVal), 0);
				_storeHrzSldrVal = trckBrHrz.Value;//水平スライダーの現在値を保存
				//グラフィックオブジェクトに背景画像を背景画像の大きさで描画する。
//-----------------------------------------------------------------------------------------------------
//ＩＮＧ：_imgPicBack.Width/Heightではなく、_スケール.Width/Heightを用いる必要がある ↓
//				           この場合だと、画像そのものの大きさ＝縮尺０↓             ↓
//-----------------------------------------------------------------------------------------------------
				_gCanvasBck.DrawImage(_imgObjBck, new Rectangle(0, 0, _imgObjBck.Width, _imgObjBck.Height));

				//PictureBox1に表示する
				picBox.Image = _canvasBck;
			}

			//垂直移動の場合
			if (edtTyp == _EdtTyp.Vertical) {
				//一旦画像クリア
				g.Clear(Color.Black);
				//水平スライダーの初期値からの移動量分、画像のワールド変換行列を水平移動
				g.TranslateTransform(0,trckBrVrt.Value - _storeVrtSldrVal);
				_storeVrtSldrVal = trckBrVrt.Value;//水平スライダーの現在値を保存
				//グラフィックオブジェクトに背景画像を背景画像の大きさで描画する。
//-----------------------------------------------------------------------------------------------------
//ＩＮＧ：_imgPicBack.Width/Heightではなく、_スケール.Width/Heightを用いる必要がある ↓
//				           この場合だと、画像そのものの大きさ＝縮尺０↓             ↓
//-----------------------------------------------------------------------------------------------------
				g.DrawImage(_imgPicBack, new Rectangle(0,0, _imgPicBack.Width, _imgPicBack.Height));

				//PictureBox1に表示する
				picBox.Image = _canvasBk;
			}
    }
}
