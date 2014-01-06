using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

// 2014-01-06
// 【注意】Application.EnableVisualStyles(); が無効の時はwatertextが機能しない。

namespace Aa.ControlEx
{

  /// <summary>
  /// テキストボックスに透かし文字（入力プロンプトやヒントとも言います）を表示する。
  /// </summary>
  [ToolboxBitmap(typeof(TextBox))]
  public class TextBoxEx : TextBox
  {

    /// <summary>透かし文字の表示・非表示の設定中に ModifiedChanged
    /// プロパティを変更しているかどうか</summary>
    private bool _isModifiedChanging;
    /// <summary>本来のテキストが空かどうか</summary>
    private bool _isEmpty;
    private string _watermarkText;
    private Color _watermarkColor;
    private Color _foregroundColor;

    public TextBoxEx()
    {
      _isEmpty = true;
      _watermarkText = "";
      _watermarkColor = Color.DarkGray;
      _foregroundColor = ForeColor;
    }

    protected override void OnValidated(EventArgs e)
    {
      base.OnValidated(e);
      base.Text = base.Text.Trim();
    }

    private void SetBaseText(string text)
    {
      bool m = Modified;
      base.Text = text;
      Modified = m;
      _isModifiedChanging = false;
    }

    protected override void OnModifiedChanged(EventArgs e)
    {
      if (_isModifiedChanging) {
        return;
      }

      base.OnModifiedChanged(e);
    }

    protected override void OnEnter(EventArgs e)
    {
      if (_isEmpty) {
        _isEmpty = false;
        base.ForeColor = ForegroundColor;
        SetBaseText("");
      }

      base.OnEnter(e);
    }

    protected override void OnLeave(EventArgs e)
    {
      base.OnLeave(e);
      base.Text = base.Text.Trim();
      if (base.Text == "" && WatermarkText != "") {
        _isEmpty = true;
        base.ForeColor = WatermarkColor;
        SetBaseText(WatermarkText);
      } else {
        _isEmpty = false;
      }
    }

    [RefreshProperties(RefreshProperties.Repaint)]
    public override string Text
    {
      get
      {
        if (_isEmpty) {
          return "";
        }

        return base.Text;
      }
      set
      {
        if (value == "" && WatermarkText != "") {
          _isEmpty = true;
          base.ForeColor = WatermarkColor;
          base.Text = WatermarkText;
        } else {
          _isEmpty = false;
          if (base.ForeColor != _foregroundColor) {
            base.ForeColor = _foregroundColor;
          }
          base.Text = value;
        }
      }
    }

    public override Color ForeColor
    {
      get
      {
        return base.ForeColor;
      }
      set
      {
        if (_isEmpty) {
          _watermarkColor = value;
        } else {
          _foregroundColor = value;
        }

        base.ForeColor = value;
      }
    }

    [Category("表示")]
    [DefaultValue(typeof(Color), "WindowText")]
    [Description("ForeColor プロパティに設定したコントロールの前景色を取得または設定します。")]
    public Color ForegroundColor
    {
      get
      {
        return _foregroundColor;
      }
      set
      {
        _foregroundColor = value;

        if (_isEmpty == false && base.ForeColor != _foregroundColor) {
          base.ForeColor = value;
        }
      }
    }

    [Category("表示")]
    [DefaultValue("")]
    [Description("テキストが空の場合に表示する文字列を設定または取得します。")]
    [RefreshProperties(RefreshProperties.Repaint)]
    public string WatermarkText
    {
      get
      {
        return _watermarkText;
      }
      set
      {
        _watermarkText = value;

        if (base.Text == "" && value != "") {
          _isEmpty = true;
          base.ForeColor = WatermarkColor;
          SetBaseText(value);
        } else if (base.Text == "" && value == "") {
          base.Text = "";
        }
      }
    }

    [Category("表示")]
    [DefaultValue(typeof(Color), "DarkGray")]
    [Description("テキストが空の場合に表示する文字列の色を設定または取得します。")]
    public Color WatermarkColor
    {
      get
      {
        return _watermarkColor;
      }
      set
      {
        _watermarkColor = value;

        if (_isEmpty) {
          base.ForeColor = value;
        }
      }
    }
  }
}
