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
        private Bitmap _canvasBk;//描画先(canvas)とするImageオブジェクト(ビットマップオブジェクト)の宣言
        private Graphics _gCanvasBk;//描画先(canvas)のGraphicsオブジェクトの宣言
        private Image _imgPicBack;//背景を描画するオブジェクトの宣言


        private double zoomRatio = 1d;//倍率
        //画像の描画領域(大きさ)と位置を保持する変数⇒領域を拡大・縮小することで画像を拡大・縮小できる。
        private Rectangle _rctBkImg = new Rectangle(0, 0, 0, 0);//背景画像用変数
        private Rectangle _rctFrImg;//前景画像用

        private int _storeHrzSldrVal; //水平スライダー移動量計算用スライダー値一時保存用(前回の値－今回の値で計算する)
        private int _storeVrtSldrVal; //垂直スライダー移動量計算用スライダー値一時保存用(前回の値－今回の値で計算する)


        public enum _EdtTyp : int {
            Horizontal = 1,
            Vertical = 2,
            Scale = 3
        }        
        
        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {


            //			if (canvas != null) canvas.Dispose();

            //宣言した描画先(canvas)とするImageオブジェクト(ビットマップオブジェクト)の実体を作成
            _canvasBk = new Bitmap(picBxBk.Width, picBxBk.Height);
            //宣言した描画先(canvas)のGraphicsオブジェクトの実態を作成
            _gCanvasBk = Graphics.FromImage(_canvasBk);

            //背景画像を読み込む
            _imgPicBack = Image.FromFile("house.jpg");

            _rctBkImg.X = 0;
            _rctBkImg.Y = 0;
            _rctBkImg.Width = _imgPicBack.Width;//描画領域を読込背景画像の大きさにセット
            _rctBkImg.Height = _imgPicBack.Height;//　　　　〃

            //トラックバーのスライダー移動量の最大値は、背景画像の大きさの１／２とする(±１／１)
            trckBrHrz.Maximum = _imgPicBack.Width / 2;
            trckBrVrt.Maximum = _imgPicBack.Height / 2;

            //トラックバーのスライダーのつまみの初期値は背景画像の大きさの１／４とする（移動量は±１／２）
            trckBrHrz.Value = _imgPicBack.Width / 4;
            trckBrVrt.Value = trckBrVrt.Maximum / 2;
            _storeHrzSldrVal = trckBrHrz.Value;//水平スライダーのつまみの位置を保存
            _storeVrtSldrVal = trckBrVrt.Value;//垂直スライダーのつまみの位置を保存

            //			zoomRatio = 1d;
            //ワールド変換行列を単位行列にリセット
            _gCanvasBk.ResetTransform();
            //画像を描画先のキャンバスの中心持ってくるため、ワールド変換行列の中心に平行移動する
            _gCanvasBk.TranslateTransform(-(_imgPicBack.Width - picBxBk.Width) / 2, -(_imgPicBack.Height - picBxBk.Height) / 2);
            //画像を描画
//            _gCanvasBk.DrawImage(_imgPicBack, _rctBkImg);


            //PictureBox1に表示する
//            picBxBk.Image = _canvasBk;
        }

        private void button1_Click(object sender, EventArgs e) {
            //PictureBoxコントロール(pictureBox1)に画像ファイル(duke.gif)を表示します。
            System.Drawing.Bitmap bm = new System.Drawing.Bitmap("house.jpg");
            //Graphicsオブジェクトのインスタンス生成
            System.Drawing.Graphics g = pictureBox1.CreateGraphics();
            //描画
            _gCanvasBk.DrawImage(bm, 0, 0);
            g.DrawImage(bm, 0, 0);
            //破棄
            bm.Dispose();
            g.Dispose();

            //宣言した描画先(canvas)とするImageオブジェクト(ビットマップオブジェクト)の実体を作成
            _canvasBk = new Bitmap(picBxBk.Width, picBxBk.Height);
            //宣言した描画先(canvas)のGraphicsオブジェクトの実態を作成
            _gCanvasBk = Graphics.FromImage(_canvasBk);

            //背景画像を読み込む
            _imgPicBack = Image.FromFile("house.jpg");


        }

    }
}
