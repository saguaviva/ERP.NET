Imports MySql.Data.MySqlClient : Imports clsFuncionesLOG : Imports clsFuncionesC1 : Imports clsFuncionesUtiles : Imports clsConstantes
Public Class clsLineasCompra
    Inherits clsADO

    'El dtColors cuando son fornituras es la mida

#Region "CAMPOS"

    '+----------+---------------------+------+-----+---------+-------+
    '| Field    | Type                | Null | Key | Default | Extra |
    '+----------+---------------------+------+-----+---------+-------+
    '| CENTRO   | char(1)             | NO   | PRI | C       |       |
    '| TIPUS    | char(1)             | NO   | PRI |         |       |
    '| DOCUMENT | char(1)             | NO   | PRI |         |       |
    '| FRA      | int(11)             | NO   | PRI | 0       |       |
    '| NLINEA   | int(11)             | NO   | PRI | 0       |       |
    '| ARTICLE  | varchar(10)         | YES  |     | NULL    |       |
    '| DESCRI   | varchar(50)         | YES  |     | NULL    |       |
    '| COLOR    | varchar(10)         | YES  |     | NULL    |       |
    '| TALLA    | varchar(10)         | YES  |     | NULL    |       |
    '| UNITATS  | double              | YES  |     | NULL    |       |
    '| PREU     | double              | YES  |     | NULL    |       |
    '| DTE      | double              | YES  |     | NULL    |       |
    '| IMPORT   | double              | YES  |     | NULL    |       |
    '| COMAN    | int(11)             | YES  |     | NULL    |       |
    '| ALBAR    | int(11)             | YES  |     | NULL    |       |
    '| FACTURA  | int(11)             | YES  |     | NULL    |       |
    '| KM       | char(1)             | YES  |     | NULL    |       |
    '| REBUT    | double(15,2)        | NO   |     | 0.00    |       |
    '| PERREBRE | double(15,2)        | YES  |     | 0.00    |       |
    '| ESTOC    | tinyint(1) unsigned | NO   |     | 0       |       |
    '+----------+---------------------+------+-----+---------+-------+
    '20 rows

    Private mTIPUS As String
    Public Property TIPUS() As String
        Get
            If PA = -1 Then Exit Property
            Try
                mTIPUS = general.nz(dvForm(PA).Row("TIPUS"), "")
            Catch ex As Exception : End Try
            Return general.nz(mTIPUS, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(TIPUS, "") Then
                mTIPUS = general.nz(Value, "")
                dvForm(PA).Row("TIPUS") = general.nz(mTIPUS, "") : guardarDV()
            End If
        End Set
    End Property

    Private mDOCUMENT As String
    Public Property DOCUMENT() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mDOCUMENT = general.nz(dvForm(PA).Row("DOCUMENT"), "")
            Catch ex As Exception : End Try
            Return general.nz(mDOCUMENT, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(DOCUMENT, "") Then
                mDOCUMENT = general.nz(Value, "")
                dvForm(PA).Row("DOCUMENT") = general.nz(mDOCUMENT, "") : guardarDV()
            End If
        End Set
    End Property

    Private mFRA As Integer
    Public Property FRA() As Integer
        Get
            If PA() = -1 Then Exit Property
            Try
                mFRA = nzn(dvForm(PA).Row("FRA"), 0)
            Catch ex As Exception : End Try
            Return nzn(mFRA, 0)
        End Get
        Set(ByVal Value As Integer)
            If PA() = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(FRA, 0) Then
                mFRA = nzn(Value, 0)
                dvForm(PA).Row("FRA") = nzn(mFRA, 0) : guardarDV()
            End If
        End Set
    End Property

    Private mNLINEA As Integer
    Public Property NLINEA() As Integer
        Get
            If PA() = -1 Then Exit Property
            Try
                mNLINEA = nzn(dvForm(PA).Row("NLINEA"), 0)
            Catch ex As Exception : End Try
            Return nzn(mNLINEA, 0)
        End Get
        Set(ByVal Value As Integer)
            If PA() = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(NLINEA, 0) Then
                mNLINEA = nzn(Value, 0)
                dvForm(PA).Row("NLINEA") = nzn(mNLINEA, 0) : guardarDV()
            End If
        End Set
    End Property

    Private mARTICLE As String
    Public Property ARTICLE() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mARTICLE = general.nz(dvForm(PA).Row("ARTICLE"), "")
            Catch ex As Exception : End Try
            Return general.nz(mARTICLE, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(ARTICLE, "") Then
                mARTICLE = general.nz(Value, "")
                dvForm(PA).Row("ARTICLE") = general.nz(mARTICLE, "") : guardarDV()
            End If
        End Set
    End Property

    Private mDESCRI As String
    Public Property DESCRI() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mDESCRI = general.nz(dvForm(PA).Row("DESCRI"), "")
            Catch ex As Exception : End Try
            Return general.nz(mDESCRI, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(DESCRI, "") Then
                mDESCRI = general.nz(Value, "")
                dvForm(PA).Row("DESCRI") = general.nz(mDESCRI, "") : guardarDV()
            End If
        End Set
    End Property

    Private mCOLOR As String
    Public Property COLOR() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mCOLOR = general.nz(dvForm(PA).Row("COLOR"), "")
            Catch ex As Exception : End Try
            Return general.nz(mCOLOR, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(COLOR, "") Then
                mCOLOR = general.nz(Value, "")
                dvForm(PA).Row("COLOR") = general.nz(mCOLOR, "") : guardarDV()
            End If
        End Set
    End Property

    Private mTALLA As String
    Public Property TALLA() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mTALLA = general.nz(dvForm(PA).Row("TALLA"), "")
            Catch ex As Exception : End Try
            Return general.nz(mTALLA, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(TALLA, "") Then
                mTALLA = general.nz(Value, "")
                dvForm(PA).Row("TALLA") = general.nz(mTALLA, "") : guardarDV()
            End If
        End Set
    End Property

    Private mUNITATS As Double
    Public Property UNITATS() As Double
        Get
            If PA() = -1 Then Exit Property
            Try
                mUNITATS = nzn(dvForm(PA).Row("UNITATS"), 0)
            Catch ex As Exception : End Try
            Return nzn(mUNITATS, 0)
        End Get
        Set(ByVal Value As Double)
            If PA() = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(UNITATS, 0) Then
                mUNITATS = nzn(Value, 0)
                dvForm(PA).Row("UNITATS") = nzn(mUNITATS, 0) : guardarDV()
            End If
        End Set
    End Property

    Private mPREU As Double
    Public Property PREU() As Double
        Get
            If PA() = -1 Then Exit Property
            Try
                mPREU = nzn(dvForm(PA).Row("PREU"), 0)
            Catch ex As Exception : End Try
            Return nzn(mPREU, 0)
        End Get
        Set(ByVal Value As Double)
            If PA() = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(PREU, 0) Then
                mPREU = nzn(Value, 0)
                dvForm(PA).Row("PREU") = nzn(mPREU, 0) : guardarDV()
            End If
        End Set
    End Property

    Private mDTE As Double
    Public Property DTE() As Double
        Get
            If PA() = -1 Then Exit Property
            Try
                mDTE = nzn(dvForm(PA).Row("DTE"), 0)
            Catch ex As Exception : End Try
            Return nzn(mDTE, 0)
        End Get
        Set(ByVal Value As Double)
            If PA() = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(DTE, 0) Then
                mDTE = nzn(Value, 0)
                dvForm(PA).Row("DTE") = nzn(mDTE, 0) : guardarDV()
            End If
        End Set
    End Property

    Private mIMPORT As Double
    Public Property IMPORT() As Double
        Get
            If PA() = -1 Then Exit Property
            Try
                mIMPORT = nzn(dvForm(PA).Row("IMPORT"), 0)
            Catch ex As Exception : End Try
            Return nzn(mIMPORT, 0)
        End Get
        Set(ByVal Value As Double)
            If PA() = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(IMPORT, 0) Then
                mIMPORT = nzn(Value, 0)
                dvForm(PA).Row("IMPORT") = nzn(mIMPORT, 0) : guardarDV()
            End If
        End Set
    End Property

    Private mCOMAN As Integer
    Public Property COMAN() As Integer
        Get
            If PA() = -1 Then Exit Property
            Try
                mCOMAN = nzn(dvForm(PA).Row("COMAN"), 0)
            Catch ex As Exception : End Try
            Return nzn(mCOMAN, 0)
        End Get
        Set(ByVal Value As Integer)
            If PA() = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(COMAN, 0) Then
                mCOMAN = nzn(Value, 0)
                dvForm(PA).Row("COMAN") = nzn(mCOMAN, 0) : guardarDV()
            End If
        End Set
    End Property

    Private mALBAR As Integer
    Public Property ALBAR() As Integer
        Get
            If PA() = -1 Then Exit Property
            Try
                mALBAR = nzn(dvForm(PA).Row("ALBAR"), 0)
            Catch ex As Exception : End Try
            Return nzn(mALBAR, 0)
        End Get
        Set(ByVal Value As Integer)
            If PA() = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(ALBAR, 0) Then
                mALBAR = nzn(Value, 0)
                dvForm(PA).Row("ALBAR") = nzn(mALBAR, 0) : guardarDV()
            End If
        End Set
    End Property

    Private mFACTURA As Integer
    Public Property FACTURA() As Integer
        Get
            If PA() = -1 Then Exit Property
            Try
                mFACTURA = nzn(dvForm(PA).Row("FACTURA"), 0)
            Catch ex As Exception : End Try
            Return nzn(mFACTURA, 0)
        End Get
        Set(ByVal Value As Integer)
            If PA() = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(FACTURA, 0) Then
                mFACTURA = nzn(Value, 0)
                dvForm(PA).Row("FACTURA") = nzn(mFACTURA, 0) : guardarDV()
            End If
        End Set
    End Property

    Private mKM As String
    Public Property KM() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mKM = general.nz(dvForm(PA).Row("KM"), "")
            Catch ex As Exception : End Try
            Return general.nz(mKM, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(KM, "") Then
                mKM = general.nz(Value, "")
                dvForm(PA).Row("KM") = general.nz(mKM, "") : guardarDV()
            End If
        End Set
    End Property

    Private mREBUT As Double
    Public Property REBUT() As Double
        Get
            If PA() = -1 Then Exit Property
            Try
                mREBUT = nzn(dvForm(PA).Row("REBUT"), 0)
            Catch ex As Exception : End Try
            Return nzn(mREBUT, 0)
        End Get
        Set(ByVal Value As Double)
            If PA() = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(REBUT, 0) Then
                mREBUT = nzn(Value, 0)
                dvForm(PA).Row("REBUT") = nzn(mREBUT, 0) : guardarDV()
            End If
        End Set
    End Property

    Private mPERREBRE As Double
    Public Property PERREBRE() As Double
        Get
            If PA() = -1 Then Exit Property
            Try
                mPERREBRE = nzn(dvForm(PA).Row("PERREBRE"), 0)
            Catch ex As Exception : End Try
            Return nzn(mPERREBRE, 0)
        End Get
        Set(ByVal Value As Double)
            If PA() = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(PERREBRE, 0) Then
                mPERREBRE = nzn(Value, 0)
                dvForm(PA).Row("PERREBRE") = nzn(mPERREBRE, 0) : guardarDV()
            End If
        End Set
    End Property

    Private mESTOC As Boolean
    Public Property ESTOC() As Boolean
        Get
            If PA() = -1 Then Exit Property
            Try
                mESTOC = dvForm(PA).Row("ESTOC")
            Catch ex As Exception : End Try
            Return mESTOC
        End Get
        Set(ByVal Value As Boolean)
            If PA() = -1 Then Exit Property
            If Value <> ESTOC Then
                mESTOC = Value
                dvForm(PA).Row("ESTOC") = mESTOC : guardarDV()
            End If
        End Set
    End Property

    'Private mTIPUS As String
    'Public Property TIPUS() As String
    '    Get
    '        If PA = -1 Then Exit Property
    '        Try
    '            mTIPUS =general.nz(dvForm(pa).Row("TIPUS"), "")
    '        Catch ex As Exception : End Try
    '        Return general.nz(mTIPUS, "")
    '    End Get
    '    Set(ByVal Value As String)
    '        If PA = -1 Then Exit Property
    '        mTIPUS =general.nz(Value, "")
    '        dvForm(pa).Row("TIPUS") =general.nz(Value, "") : GuardarDV()
    '    End Set
    'End Property

    'Private mDOCUMENT As String
    'Public Property DOCUMENT() As String
    '    Get
    '        If PA = -1 Then Exit Property
    '        Try
    '            mDOCUMENT =general.nz(dvForm(pa).Row("DOCUMENT"), "")
    '        Catch ex As Exception : End Try
    '        Return general.nz(mDOCUMENT, "")
    '    End Get
    '    Set(ByVal Value As String)
    '        If PA = -1 Then Exit Property
    '        mDOCUMENT =general.nz(Value, "")
    '        dvForm(pa).Row("DOCUMENT") =general.nz(Value, "") : GuardarDV()
    '    End Set
    'End Property

    'Private mFRA As Integer
    'Public Property FRA() As Integer
    '    Get
    '        If PA = -1 Then Exit Property
    '        Try
    '            mFRA = nzn(dvForm(pa).Row("FRA"), 0)
    '        Catch ex As Exception : End Try
    '        Return nzn(mFRA, 0)
    '    End Get
    '    Set(ByVal Value As Integer)
    '        If PA = -1 Then Exit Property
    '        mFRA = nzn(Value, 0)
    '        dvForm(pa).Row("FRA") = nzn(Value, 0) : GuardarDV()
    '    End Set
    'End Property

    'Private mNLINEA As Integer
    'Public Property NLINEA() As Integer
    '    Get
    '        If PA = -1 Then Exit Property
    '        Try
    '            mNLINEA = nzn(dvForm(pa).Row("NLINEA"), 0)
    '        Catch ex As Exception : End Try
    '        Return nzn(mNLINEA, 0)
    '    End Get
    '    Set(ByVal Value As Integer)
    '        If PA = -1 Then Exit Property
    '        mNLINEA = nzn(Value, 0)
    '        dvForm(pa).Row("NLINEA") = nzn(Value, 0) : GuardarDV()
    '    End Set
    'End Property

    'Private mARTICLE As String
    'Public Property ARTICLE() As String
    '    Get
    '        If PA = -1 Then Exit Property
    '        Try
    '            mARTICLE =general.nz(dvForm(pa).Row("ARTICLE"), "")
    '        Catch ex As Exception : End Try
    '        Return general.nz(mARTICLE, "")
    '    End Get
    '    Set(ByVal Value As String)
    '        If PA = -1 Then Exit Property
    '        mARTICLE =general.nz(Value, "")
    '        dvForm(pa).Row("ARTICLE") =general.nz(Value, "") : GuardarDV()
    '    End Set
    'End Property

    'Private mDESCRI As String
    'Public Property DESCRI() As String
    '    Get
    '        If PA = -1 Then Exit Property
    '        Try
    '            mDESCRI =general.nz(dvForm(pa).Row("DESCRI"), "")
    '        Catch ex As Exception : End Try
    '        Return general.nz(mDESCRI, "")
    '    End Get
    '    Set(ByVal Value As String)
    '        If PA = -1 Then Exit Property
    '        mDESCRI =general.nz(Value, "")
    '        dvForm(pa).Row("DESCRI") =general.nz(Value, "") : GuardarDV()
    '    End Set
    'End Property

    'Private mCOLOR As String
    'Public Property COLOR() As String
    '    Get
    '        If PA = -1 Then Exit Property
    '        Try
    '            mCOLOR =general.nz(dvForm(pa).Row("COLOR"), "")
    '        Catch ex As Exception : End Try
    '        Return general.nz(mCOLOR, "")
    '    End Get
    '    Set(ByVal Value As String)
    '        If PA = -1 Then Exit Property
    '        mCOLOR =general.nz(Value, "")
    '        dvForm(pa).Row("COLOR") =general.nz(Value, "") : GuardarDV()
    '    End Set
    'End Property

    'Private mTALLA As String
    'Public Property TALLA() As String
    '    Get
    '        If PA = -1 Then Exit Property
    '        Try
    '            mTALLA =general.nz(dvForm(pa).Row("TALLA"), "")
    '        Catch ex As Exception : End Try
    '        Return general.nz(mTALLA, "")
    '    End Get
    '    Set(ByVal Value As String)
    '        If PA = -1 Then Exit Property
    '        mTALLA =general.nz(Value, "")
    '        dvForm(pa).Row("TALLA") =general.nz(Value, "") : GuardarDV()
    '    End Set
    'End Property

    'Private mUNITATS As Double
    'Public Property UNITATS() As Double
    '    Get
    '        If PA = -1 Then Exit Property
    '        Try
    '            mUNITATS = nzn(dvForm(pa).Row("UNITATS"), 0)
    '        Catch ex As Exception : End Try
    '        Return nzn(mUNITATS, 0)
    '    End Get
    '    Set(ByVal Value As Double)
    '        If PA = -1 Then Exit Property
    '        mUNITATS = nzn(Value, 0)
    '        dvForm(pa).Row("UNITATS") = nzn(Value, 0) : GuardarDV()
    '    End Set
    'End Property

    'Private mPREU As Double
    'Public Property PREU() As Double
    '    Get
    '        If PA = -1 Then Exit Property
    '        Try
    '            mPREU = nzn(dvForm(pa).Row("PREU"), 0)
    '        Catch ex As Exception : End Try
    '        Return nzn(mPREU, 0)
    '    End Get
    '    Set(ByVal Value As Double)
    '        If PA = -1 Then Exit Property
    '        mPREU = nzn(Value, 0)
    '        dvForm(pa).Row("PREU") = nzn(Value, 0) : GuardarDV()
    '    End Set
    'End Property

    'Private mDTE As Double
    'Public Property DTE() As Double
    '    Get
    '        If PA = -1 Then Exit Property
    '        Try
    '            mDTE = nzn(dvForm(pa).Row("DTE"), 0)
    '        Catch ex As Exception : End Try
    '        Return nzn(mDTE, 0)
    '    End Get
    '    Set(ByVal Value As Double)
    '        If PA = -1 Then Exit Property
    '        mDTE = nzn(Value, 0)
    '        dvForm(pa).Row("DTE") = nzn(Value, 0) : GuardarDV()
    '    End Set
    'End Property

    'Private mIMPORT As Double
    'Public Property IMPORT() As Double
    '    Get
    '        If PA = -1 Then Exit Property
    '        Try
    '            mIMPORT = nzn(dvForm(pa).Row("IMPORT"), 0)
    '        Catch ex As Exception : End Try
    '        Return nzn(mIMPORT, 0)
    '    End Get
    '    Set(ByVal Value As Double)
    '        If PA = -1 Then Exit Property
    '        If dvForm(pa).Row("IMPORT") <> nzn(Value, 0) Then
    '            mIMPORT = nzn(Value, 0)
    '            dvForm(pa).Row("IMPORT") = nzn(Value, 0) : guardarDV()
    '        End If
    '    End Set
    'End Property

    'Private mCOMAN As Integer
    'Public Property COMAN() As Integer
    '    Get
    '        If PA = -1 Then Exit Property
    '        Try
    '            mCOMAN = nzn(dvForm(pa).Row("COMAN"), 0)
    '        Catch ex As Exception : End Try
    '        Return nzn(mCOMAN, 0)
    '    End Get
    '    Set(ByVal Value As Integer)
    '        If PA = -1 Then Exit Property
    '        mCOMAN = nzn(Value, 0)
    '        dvForm(pa).Row("COMAN") = nzn(Value, 0) : GuardarDV()
    '    End Set
    'End Property

    'Private mALBAR As Integer
    'Public Property ALBAR() As Integer
    '    Get
    '        If PA = -1 Then Exit Property
    '        Try
    '            mALBAR = nzn(dvForm(pa).Row("ALBAR"), 0)
    '        Catch ex As Exception : End Try
    '        Return nzn(mALBAR, 0)
    '    End Get
    '    Set(ByVal Value As Integer)
    '        If PA = -1 Then Exit Property
    '        mALBAR = nzn(Value, 0)
    '        dvForm(pa).Row("ALBAR") = nzn(Value, 0) : GuardarDV()
    '    End Set
    'End Property

    'Private mFACTURA As Integer
    'Public Property FACTURA() As Integer
    '    Get
    '        If PA = -1 Then Exit Property
    '        Try
    '            mFACTURA = nzn(dvForm(pa).Row("FACTURA"), 0)
    '        Catch ex As Exception : End Try
    '        Return nzn(mFACTURA, 0)
    '    End Get
    '    Set(ByVal Value As Integer)
    '        If PA = -1 Then Exit Property
    '        mFACTURA = nzn(Value, 0)
    '        dvForm(pa).Row("FACTURA") = nzn(Value, 0) : GuardarDV()
    '    End Set
    'End Property

    'Private mKM As String
    'Public Property KM() As String
    '    Get
    '        If PA = -1 Then Exit Property
    '        Try
    '            mKM =general.nz(dvForm(pa).Row("KM"), "")
    '        Catch ex As Exception : End Try
    '        Return general.nz(mKM, "")
    '    End Get
    '    Set(ByVal Value As String)
    '        If PA = -1 Then Exit Property
    '        mKM =general.nz(Value, "")
    '        dvForm(pa).Row("KM") =general.nz(Value, "") : GuardarDV()
    '    End Set
    'End Property

    'Private mREBUT As Double
    'Public Property REBUT() As Double
    '    Get
    '        If PA = -1 Then Exit Property
    '        Try
    '            mREBUT = nzn(dvForm(pa).Row("REBUT"), 0)
    '        Catch ex As Exception : End Try
    '        Return nzn(mREBUT, 0)
    '    End Get
    '    Set(ByVal Value As Double)
    '        If PA = -1 Then Exit Property
    '        mREBUT = nzn(Value, 0)
    '        dvForm(pa).Row("REBUT") = nzn(Value, 0) : GuardarDV()
    '    End Set
    'End Property

    'Private mPERREBRE As Double
    'Public Property PERREBRE() As Double
    '    Get
    '        If PA = -1 Then Exit Property
    '        Try
    '            mPERREBRE = nzn(dvForm(pa).Row("PERREBRE"), 0)
    '        Catch ex As Exception : End Try
    '        Return nzn(mPERREBRE, 0)
    '    End Get
    '    Set(ByVal Value As Double)
    '        If PA = -1 Then Exit Property
    '        mPERREBRE = nzn(Value, 0)
    '        dvForm(pa).Row("PERREBRE") = nzn(Value, 0) : GuardarDV()
    '    End Set
    'End Property

    'Private mESTOC As Boolean
    'Public Property ESTOC() As Boolean
    '    Get
    '        If PA = -1 Then Exit Property
    '        Try
    '            mESTOC = dvForm(pa).Row("ESTOC")
    '        Catch ex As Exception : End Try
    '        Return mESTOC
    '    End Get
    '    Set(ByVal Value As Boolean)
    '        If PA = -1 Then Exit Property
    '        mESTOC = Value
    '        dvForm(pa).Row("ESTOC") = Value : GuardarDV()
    '    End Set
    'End Property


#End Region

#Region "VARIABLES"


    Public PAF As clsPAFCompra
    Public dtTalla As New DataTable("TALLA")
    Public dtColores As New DataTable("COLOR")
    Public dtMedidas As New DataTable("MEDIDA")
    Public dtKM As New DataTable("KM")

    Friend dtArticulos As New DataTable("ARTICULOS")
    Public dvArticulos As New DataView

#End Region

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(ByVal tabla As DataTable, _
                ByVal centro As String, ByRef bindingcontext As BindingContext, ByVal p As clsPAFCompra)

        MyBase.New(tabla, centro, bindingcontext, "ESDETALLE")
        Dim sqlSel As String
        Try
            PAF = p

            sqlSinWhere = "SELECT DCACTU.*, " & _
                            " filiales.DESCRI AS NOMCENTRO " & _
                            " FROM DCACTU " & _
                            " LEFT JOIN filiales ON (filiales.CODI = DCACTU.CENTRO) "

            sqlSel = sqlSinWhere & _
                        " WHERE DCACTU.FRA = """ & PAF.FRA & """ " & _
                        " AND DCACTU.DOCUMENT = """ & PAF.DOCUMENT & """ AND DCACTU.TIPUS = """ & PAF.TIPUS & """ AND " & _
                        " DCACTU.CENTRO = """ & PAF.centro & """ ORDER BY DCACTU.FRA "
            cmdSel.CommandText = sqlSel

            dvForm.Sort = "NLINEA ASC"
            da.SelectCommand = cmdSel
            da.Fill(tabla)

            If Not tabla.Columns.Contains("ELEGIRTEJIDO") Then
                tabla.Columns.Add("ELEGIRTEJIDO")
                tabla.Columns.Add("RECEPCION")
                tabla.Columns("RECEPCION").DefaultValue = rm.GetString("AFEGIRREBUT")
            End If

            CreaTablas()
            PonerDefaults()
            AddHandler tabla.RowChanged, AddressOf RowChanged
            tabla.AcceptChanges()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#Region "ORGANIZAR"

    Private Sub RowChanged(ByVal sender As Object, ByVal e As System.Data.DataRowChangeEventArgs)

        Try
            If NLINEA = 10000 Then
                Try
                    If dvForm(dvForm.Count - 2).Item("NLINEA") = 10000 Then
                        NLINEA = 1
                    Else
                        NLINEA = dvForm(dvForm.Count - 2).Item("NLINEA") + 1
                    End If

                Catch ex As Exception
                    NLINEA = 1
                End Try
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub PonerDefaults()
        Try
            With dvForm.Table

                .Columns("CENTRO").DefaultValue = PAF.centro
                .Columns("TIPUS").DefaultValue = PAF.TIPUS
                .Columns("DOCUMENT").DefaultValue = PAF.DOCUMENT
                .Columns("FRA").DefaultValue = PAF.FRA
                .Columns("NLINEA").DefaultValue = 10000
                .Columns("ARTICLE").DefaultValue = ""
                .Columns("DESCRI").DefaultValue = ""
                .Columns("COLOR").DefaultValue = ""
                .Columns("UNITATS").DefaultValue = 0
                .Columns("PREU").DefaultValue = 0
                .Columns("DTE").DefaultValue = 0
                .Columns("IMPORT").DefaultValue = 0
                .Columns("COMAN").DefaultValue = 0
                .Columns("ALBAR").DefaultValue = 0
                .Columns("FACTURA").DefaultValue = 0
                .Columns("ESTOC").DefaultValue = 0
                .Columns("REBUT").DefaultValue = 0
                .Columns("PERREBRE").DefaultValue = 0
                .Columns("RECEPCION").DefaultValue = ""
                If PAF.TIPUS = "T" Then
                    .Columns("KM").DefaultValue = ""
                End If
                .Columns("ELEGIRTEJIDO").DefaultValue = ""
            End With
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Function ObtenerClaveArticulo() As String()
        Dim i As Integer
        Try
            If PAF.TIPUS = "M" Then : i = 1
            Else : i = 0
            End If

            Dim key(i) As String

            key(0) = ARTICLE
            If PAF.TIPUS = "M" Then
                key(1) = PAF.PROVE
            End If

            Return key
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Function
    Private Sub CambioArticulo()
        Try
            If Not DOCUMENT = OrdenFabComplementos Then

                Select Case PAF.TIPUS
                    Case "T"
                        dtColores.Rows.Clear()
                        COLOR = ""
                        KM = ""

                    Case "M"
                        dtColores.Rows.Clear()
                        dtTalla.Rows.Clear()
                        COLOR = ""
                        TALLA = ""
                    Case Fornitura
                        dtTalla.Rows.Clear()
                        TALLA = ""
                        COLOR = ""
                End Select
                PREU = 0
                IMPORT = 0
                DTE = 0
                PonerDescripcion()
                CargarColores()

            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#End Region

#Region "TABLAS ELECCION"

    Private Sub CreaTablas()
        CrearTablaColor()
        CrearTablaTallas()
        CrearTablaKM()

        If TIPUS = Fornitura Then CrearTablaMedida()

        CreaTablaArticulos()

    End Sub
    Private Sub CrearTablaColor()
        Dim dc1 As New DataColumn("COLOR")
        Try
            dc1.Caption = rm.GetString("COLOR")
            dtColores.Columns.Add(dc1)

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub CrearTablaMedida()
        Dim dc1 As New DataColumn("MEDIDA")
        Try
            dc1.Caption = rm.GetString("MEDIDA")
            dtMedidas.Columns.Add(dc1)

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub CrearTablaTallas()
        Dim dc1 As New DataColumn("TALLA")
        Try
            dc1.Caption = rm.GetString("TALLA")
            dtTalla.Columns.Add(dc1)

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub CrearTablaKM()
        Dim dc1 As New DataColumn("KM")
        Dim dc2 As New DataColumn("NOMKM")
        Dim dr As DataRow
        Try
            dc1.Caption = ""
            dc2.Caption = rm.GetString("KM")
            dtKM.Columns.Add(dc1)
            dtKM.Columns.Add(dc2)
            dr = dtKM.NewRow
            dr("KM") = "K"
            dr("NOMKM") = rm.GetString("KILOS")
            dtKM.Rows.Add(dr)
            dr = dtKM.NewRow
            dr("KM") = "M"
            dr("NOMKM") = rm.GetString("METROS")
            dtKM.Rows.Add(dr)

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub CreaTablaArticulos()
        Try
            Dim dc As New DataColumn("CODI")
            dc.Caption = rm.GetString("CODIGO")
            dc.DefaultValue = ""
            Dim dc2 As New DataColumn("DESCRI")
            dc2.Caption = rm.GetString("DESCRIPCION")
            dc.DefaultValue = ""
            Dim dc3 As New DataColumn("PROVE")

            If PAF.DOCUMENT = OrdenFabComplementos Then
                dc3.Caption = rm.GetString("TALLER")
            Else
                dc3.Caption = rm.GetString("PROVEEDOR")
            End If

            dc.DefaultValue = 0
            Dim dc4 As New DataColumn("NOMPROVE")
            If PAF.DOCUMENT = OrdenFabComplementos Then
                dc4.Caption = rm.GetString("NOMTALLER")
            Else
                dc4.Caption = rm.GetString("NOMPROVE")
            End If


            dc.DefaultValue = ""
            dtArticulos.Clear()
            dtArticulos.Columns.Clear()
            dtArticulos.Columns.Add(dc)
            dtArticulos.Columns.Add(dc2)
            dtArticulos.Columns.Add(dc3)
            dtArticulos.Columns.Add(dc4)

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#End Region

#Region "OVERRIDES"

    Friend Overrides Function TieneCambios() As Boolean
        Try
            guardarDV()
            If Not tabla.GetChanges Is Nothing Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Function
    Public Overrides Sub borrar()
        Try
            BorrarActualDV()
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Friend Overrides Function genWhere() As String
        Try
            Dim ret As String

            ret = "WHERE " & tabla.TableName & ".CENTRO = """ & centro & """"

            Return ret
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Function
    Friend Overrides Function GenOrder() As String
        Try
            Return ""
            'Return " ORDER BY TEMPORADA, CLIENT, SERIE, CODI "
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Function
    Friend Overrides Function ObtenerNumeroRegistro(ByVal id As Object) As Integer
        If id Is Nothing Then
            id = FRA
        End If

        Dim cmd As New MySqlCommand(" SELECT " & _
           " (SELECT COUNT(*) " & _
           " FROM " & tabla.TableName & " AS M2 WHERE " & _
           " M2.CODI < M1.CODI AND  " & WCNoTabla() & " ) AS rownum FROM " & tabla.TableName & " AS M1  WHERE CODI = """ & id & """ AND " & WCNoTabla() & GenOrder(), cnn)
        Try
            Dim idx As Object = cmd.ExecuteScalar()
            If idx Is Nothing Then Return -1
            Return idx '- 1

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Function
    Friend Overrides Function genWhereNumeroRegistros() As String
        Try
            Return genWhere()
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Function
    Friend Sub CambioDetalle(ByVal centro As String, ByVal PA As clsPAFCompra)
        Dim sqlSel As String
        Try
            PAF = PA
            Me.centro = PAF.centro
            sqlSel = sqlSinWhere & _
                        " WHERE DCACTU.FRA = """ & PAF.FRA & """ " & _
                        " AND DCACTU.DOCUMENT = """ & PAF.DOCUMENT & """ AND " & _
                        " DCACTU.TIPUS = """ & PAF.TIPUS & """ AND " & _
                        " DCACTU.CENTRO = """ & PAF.centro & """ ORDER BY DCACTU.FRA "

            cmdSel.CommandText = sqlSel
            da.SelectCommand = cmdSel
            dvForm.Sort = "NLINEA ASC"
            tabla.Clear()
            da.Fill(tabla)
            PonerDefaults()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Public Sub ActualizarDetalle()
        Dim i As Integer
        Dim cambio As Boolean = False
        Try
            For i = 0 To dvForm.Count - 1
                If nzn(dvForm(i).Item("FRA"), 0) <> nzn(PAF.FRA, 0) Then dvForm(i).Item("FRA") = nzn(PAF.FRA, 0) : cambio = True
                If general.nz(dvForm(i).Item("TIPUS"), "") <> general.nz(PAF.TIPUS, "") Then dvForm(i).Item("TIPUS") = general.nz(PAF.TIPUS, "") : cambio = True
                If general.nz(dvForm(i).Item("DOCUMENT"), "") <> general.nz(PAF.DOCUMENT, "") Then dvForm(i).Item("DOCUMENT") = general.nz(PAF.DOCUMENT, "") : cambio = True
                If general.nz(dvForm(i).Item("CENTRO"), "") <> general.nz(PAF.centro, "") Then dvForm(i).Item("CENTRO") = general.nz(PAF.centro, "") : cambio = True
            Next
            If cambio Then guardarDV()

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Sub
    Public Overrides Sub ActualizarOrigen(Optional ByVal nocerrar As Boolean = False, Optional ByVal hackDetalle As Boolean = False)
        Try
            ActualizarDetalle()
            MyBase.ActualizarOrigen(True, True)

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Sub

#End Region

#Region "CARGAR COLORES"

    Private Sub CargarColores()
        Dim i As Integer
        Dim strSQL As String
        Dim cmdSelect As New MySqlCommand
        Dim dr As DataRow
        Dim daReader As MySqlDataReader
        Try

            If Not ARTICLE = "" Then
                dtColores.Clear()

                cmdSelect.Connection = cnn
                Select Case DOCUMENT
                    Case OrdenFabComplementos
                        'strSQL = "SELECT COLOR FROM TALLA WHERE CLIENT =  "
                    Case Else
                        Select Case PAF.TIPUS
                            Case Tejido
                                strSQL = "SELECT color FROM " & tablaHiloColores & " " & _
                                    " WHERE (" & _
                                    " TIPUS =""T"" AND " & _
                                    " FIL = """ & ARTICLE & """ AND " & _
                                    " CENTRO = """ & PAF.centro & """) " & _
                                    " ORDER BY color"
                            Case Hilos
                                strSQL = "SELECT color FROM " & tablaHiloColores & " " & _
                                    " WHERE (" & _
                                    " TIPUS =""F"" AND " & _
                                    " FIL = """ & ARTICLE & """ AND " & _
                                    " PROVE = """ & PAF.PROVE & """ AND " & _
                                    " CENTRO = """ & PAF.centro & """) ORDER BY color"
                            Case Fornitura
                                strSQL = "SELECT COLOR FROM " & tablaHiloColores & " " & _
                                    " WHERE (" & _
                                    " TIPUS = ""O"" AND " & _
                                    " FIL = """ & ARTICLE & """ AND " & _
                                    " CENTRO = """ & PAF.centro & """) " & _
                                    " ORDER BY COLOR"
                        End Select
                End Select

                cmdSelect.CommandText = strSQL
                ACN()
                daReader = cmdSelect.ExecuteReader
                If TIPUS = Tejido Then
                    dr = dtColores.NewRow
                    dr("COLOR") = "**CRU**"
                    dtColores.Rows.Add(dr)
                End If
                While daReader.Read
                    dr = dtColores.NewRow
                    dr("COLOR") = daReader("COLOR")
                    dtColores.Rows.Add(dr)
                End While
                daReader.Close()
                CCN()
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#End Region

#Region "CARGAR MEDIDAS FORNITURAS"

    Private Sub CargarMedidas()
        Dim i As Integer
        Dim strSQL As String
        Dim cmdSelect As New MySqlCommand
        Dim dr As DataRow
        Dim daReader As MySqlDataReader
        Try

            If Not ARTICLE = "" AndAlso Not COLOR = "" Then
                dtMedidas.Clear()
                cmdSelect.Connection = cnn
                strSQL = "SELECT MEDIDA FROM " & tablaHiloColores & " " & _
                    " WHERE (" & _
                    " TIPUS = ""O"" AND " & _
                    " FIL = """ & ARTICLE & """ AND " & _
                    " CENTRO = """ & PAF.centro & """) " & _
                    " ORDER BY MEDIDA"

                cmdSelect.CommandText = strSQL
                ACN()
                daReader = cmdSelect.ExecuteReader

                While daReader.Read
                    dr = dtMedidas.NewRow
                    dr("MEDIDA") = daReader("MEDIDA")
                    dtMedidas.Rows.Add(dr)
                End While
                daReader.Close()
                CCN()
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#End Region

#Region "PONER PRECIOS"

    Private Sub PonerPrecioTejido()
        Dim cmdSel As MySqlCommand
        Dim daReader As MySqlDataReader
        Dim r As Double
        Try
            If Not ARTICLE = "" And Not COLOR = "" And Not KM = "" And PAF.soloClase = False Then

                cmdSel = New MySqlCommand("SELECT " & _
                                    "preum, " & _
                                    "preuk, " & _
                                    "pteixir, " & _
                                    "teixidor, rendiment " & _
                                    "FROM " & tablaTejidos & " " & _
                                    "WHERE " & _
                                    "CODI = """ & ARTICLE & """ AND CENTRO = """ & PAF.centro & """ ", cnn)
                ACN()
                daReader = cmdSel.ExecuteReader

                While daReader.Read
                    If nzn(daReader("TEIXIDOR"), 0) = PAF.PROVE Then
                        If KM = "K" Then
                            'SON METROS
                            PREU = nzn(daReader("PTEIXIR"), 0)
                        Else
                            'SON METROS
                            r = nzn(daReader("RENDIMENT"), 1)
                            If r = 0 Then r = 1
                            PREU = nzn(daReader("PTEIXIR"), 0) / r
                        End If

                    Else
                        If KM = "M" Then
                            'SON METROS

                            PREU = nzn(daReader("PREUM"), 0)
                        Else
                            'SON KILOS
                            r = nzn(daReader("RENDIMENT"), 1)
                            If r = 0 Then r = 1
                            PREU = nzn(daReader("PREUM"), 0) * r
                        End If
                    End If

                End While

                daReader.Close()
                CCN()
            End If
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
            CCN()
            daReader.Close()
        End Try
    End Sub
    Private Sub PonerPrecioHilo()
        Dim cmdSelect As New MySqlCommand
        Try

            'Estamos en el caso de las fornituras por lo que hay que poner el precio
            If Not ARTICLE = "" And Not COLOR = "" Then
                cmdSelect.Connection = cnn
                cmdSelect.CommandText = "SELECT PREUCOST FROM " & tablaHiloColores & " " & _
                    " WHERE TIPUS = ""F"" AND FIL = """ & ARTICLE & """ AND PROVE = " & PAF.PROVE & " AND CENTRO = """ & PAF.centro & """ "
                'MessageBox.Show(cmdSelect.ExecuteScalar.GetType.ToString)
                ACN()
                PREU = nzn(cmdSelect.ExecuteScalar, 0)
                CCN()
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub PonerPrecioFornitura()
        Dim cmdSelect As New MySqlCommand
        Try
            'Estamos en el caso de las fornituras por lo que hay que poner el precio
            If Not ARTICLE = "" AndAlso Not TALLA = "" Then
                cmdSelect.Connection = cnn
                cmdSelect.CommandText = "SELECT PREU FROM " & tablaHiloColores & " WHERE (MEDIDA = """ & TALLA & """ AND TIPUS = ""O"" AND FIL = """ & ARTICLE & """ AND CENTRO = """ & PAF.centro & """)"
                ACN()
                PREU = nzn(cmdSelect.ExecuteScalar, 0)
                CCN()
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub PonerDescripcion()
        Try
            If Not ARTICLE = "" Then
                Dim dr() As DataRow = dtArticulos.Select("CODI = '" & ARTICLE & "'")
                If dr.Length > 0 Then
                    DESCRI = general.nz(dr(0).Item("DESCRI"), "")
                End If
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : cnn.Close()
        End Try
    End Sub

#End Region

#Region "CALCULOS"

    Public Function esColParaActualizarPrecio(ByVal col As String) As Boolean
        Try
            If col = "ARTICLE" OrElse col = "COLOR" OrElse col = "TALLA" OrElse col = "KM" Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False
        End Try
    End Function
    Private Function CamposParaObtenerPrecioRellenos() As Boolean
        Try

            If TIPUS = "F" Then
                If ARTICLE <> "" AndAlso TALLA <> "" Then
                    Return True
                Else
                    Return False
                End If
            End If

            If ARTICLE <> "" AndAlso COLOR <> "" AndAlso TIPUS <> "" Then
                If TIPUS = "T" Then
                    If KM <> "" Then Return True
                Else
                    Return True
                End If
            End If


            'Si llegamos aqui no podemos poner precio
            Return False

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False
        End Try
    End Function
    Public Sub CambioFila()
        Try
            If Not DOCUMENT = OrdenFabComplementos Then CargarColores()

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Sub
    Public Sub HacerCalculosLineasPAF(ByVal columnaModificada As String)
        Try
            If Not DOCUMENT = OrdenFabComplementos Then
                PAF.DormirHandlers()

                Select Case columnaModificada
                    Case "ARTICLE" : CambioArticulo()
                    Case "COLOR" : CargarMedidas()
                    Case Else
                End Select

                If esColParaActualizarPrecio(columnaModificada) AndAlso CamposParaObtenerPrecioRellenos() Then
                    Select Case PAF.TIPUS
                        Case "T" : PonerPrecioTejido()
                        Case "M" : PonerPrecioHilo()
                        Case "F" : PonerPrecioFornitura()
                    End Select
                End If

                If DOCUMENT = "C" Then
                    PERREBRE = UNITATS - REBUT
                    REBUT = UNITATS - PERREBRE
                End If

                IMPORT = roundnum(PREU * (1 - DTE / 100) * UNITATS, 2)

                PAF.ActualizarTotalesPAF()
                PAF.DespertarHandlers()
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try

    End Sub

#End Region

#Region "AÑADIR MANUALMENTE"

    Public Sub AñadirLinea(ByVal articulo As String, ByVal color As String, ByVal unidades As Double, Optional ByVal kilometres As String = "", Optional ByVal precioTejido As Double = 0, Optional ByVal redondearArriba As Boolean = False)
        Dim dr As DataRow
        Dim tempRound As Double
        Try
            dr = tabla.NewRow
            dr("FRA") = PAF.FRA
            dr("TIPUS") = PAF.TIPUS
            dr("DOCUMENT") = PAF.DOCUMENT
            dr("ARTICLE") = articulo
            dr("COLOR") = color

            If redondearArriba Then
                If Math.Round(unidades, 0) <> unidades Then
                    dr("UNITATS") = Math.Round(unidades, 0) + 1
                Else
                    dr("UNITATS") = unidades
                End If
            Else
                dr("UNITATS") = unidades
            End If

            If kilometres <> "" Then
                dr("KM") = kilometres
            End If
            If precioTejido <> 0 Then
                dr("PREU") = precioTejido
            End If

            Try
                dr("NLINEA") = dvForm(dvForm.Count - 1).Item("NLINEA") + 1
            Catch ex As Exception
                dr("NLINEA") = 1
            End Try

            dr.Item("CENTRO") = PAF.centro

            Try
                tabla.Rows.Add(dr)

                RowChanged(Nothing, Nothing)
                Dim bm As BindingManagerBase
                Dim drv As DataRowView
                bm = bc(dvForm)
                If Not bm.Count = 0 Then
                    bm.Position = bm.Count - 1
                End If
                drv = CType(bm.Current, DataRowView)
                PonerDescripcion()
                HacerCalculosLineasPAF("COLOR")
                MyBase.ActualizarOrigen(True, True)

            Catch ex As Exception
                Throw ex
            End Try

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#End Region

#Region "ORGANIZAR"

    '*****************************************************************************
    'A esta funcion no hace falta pasarle el tipo pq los paf no influye para la nuemeracion
    '*****************************************************************************
    Friend Sub AñadirDesplegableArticulos(ByVal tipo As String, ByVal documento As String, Optional ByVal COMPRAOVENTA As Char = "C", Optional ByVal client As Integer = -1, Optional ByVal nocerrarconexion As Boolean = False)
        Dim i As Integer
        Dim cmdSelect As New MySqlCommand
        Dim dr As DataRow
        Dim daReader As MySqlDataReader
        Try
            'No pone los repetidos es para los PAF, tampoco los nulos q no deberia haber
            cmdSelect.Connection = cnn
            Select Case documento
                Case OrdenFabComplementos
                    'Cargar muestras
                    'Esto es muy basto demasiadas muestras
                    Exit Sub
                    cmdSelect.CommandText = "SELECT CODI, DESCRI, CLIENT FROM MOSTRES WHERE CENTRO = """ & PAF.centro & """ ORDER BY CODI"
                Case "F"
                    'No existen facturas de compra
                Case Albaran, Pedido
                    'Cargar Hilos, tejidos, o fornituras
                    Select Case tipo
                        Case Tejido
                            'El caso mas facil es que sea un tejido simplemente elegimos todos los tejidos
                            cmdSelect.CommandText = "SELECT CODI, DESCRI FROM teixits WHERE CENTRO = """ & PAF.centro & """ ORDER BY CODI"

                        Case Muestra
                            'Si son muestras cogemos todas las muestras que tenemos
                            If COMPRAOVENTA = "V" Then
                                cmdSelect.CommandText = "SELECT CODI, DESCRI, CLIENT FROM MOSTRES WHERE CENTRO = """ & PAF.centro & """ ORDER BY CODI"
                            Else
                                'Quiere decir que son compras de hilos por lo tanto hemos de coger los hilos
                                'If Not DOCUMENTO = "C" Then : strSQL = "SELECT CODI, DESCRI FROM MOSTRES ORDER BY CODI"
                                'strSQL = "SELECT CODI, DESCRI, PROVE FROM fil ORDER BY CODI"
                                cmdSelect.CommandText = "SELECT fil.CODI, fil.DESCRI, fil.PROVE, " & _
                                   " prove.NOM AS NOMPROVE FROM fil " & _
                                   " LEFT JOIN prove ON (prove.CODI = fil.PROVE)  " & _
                                   " WHERE fil.CENTRO = """ & PAF.centro & """ AND PROVE = " & PAF.PROVE & " ORDER BY CODI"
                            End If

                        Case Fornitura
                            'Esto para si son pedidos de fornituras
                            cmdSelect.CommandText = "SELECT CODI, DESCRI FROM forni WHERE CENTRO = """ & PAF.centro & """ ORDER BY CODI"
                    End Select
            End Select

            ACN()
            dtArticulos.Clear()

            daReader = cmdSelect.ExecuteReader

            While daReader.Read
                dr = dtArticulos.NewRow
                dr("CODI") = daReader("CODI")
                dr("DESCRI") = daReader("DESCRI")
                'Cargamos el proveedor si es un para un hilo (compra)
                If tipo = Muestra AndAlso Not COMPRAOVENTA = "V" AndAlso documento <> OrdenFabComplementos Then
                    dr("PROVE") = daReader("PROVE")
                    dr("NOMPROVE") = daReader("NOMPROVE")
                End If
                If tipo = Muestra And COMPRAOVENTA = "V" Then dr(tablaProveedores) = daReader("CLIENT")
                dtArticulos.Rows.Add(dr)
            End While
            daReader.Close()

            If Not nocerrarconexion Then CCN()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Public Sub IniciarDesplegableArticulo()
        Try
            If Not PAF.DOCUMENT Is Nothing Then
                AñadirDesplegableArticulos(PAF.TIPUS, PAF.DOCUMENT, "C", -1, True)
                dvArticulos = dtArticulos.DefaultView

                If PAF.TIPUS = Muestra Then : dvArticulos.Sort = "CODI, PROVE"
                Else : dvArticulos.Sort = "CODI" : End If
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#End Region

End Class

'Public dtTipo As New DataTable("TIPUS")
'Public dtArticulos As New DataTable("CODI")

'Private Sub CrearTablaTIPO()
'    Dim dc1 As New DataColumn("TIPUS")
'    Dim dr As DataRow
'    Try
'        dtTipo.Columns.Add(dc1)
'        dr = dtTipo.NewRow
'        dr("TIPUS") = "T"
'        dtTipo.Rows.Add(dr)
'        dr = dtTipo.NewRow
'        dr("TIPUS") = "M"
'        dtTipo.Rows.Add(dr)
'    Catch ex As Exception
'        LOG(ex.ToString) : cargando = False : CCN()
'    End Try
'End Sub

