Imports MySql.Data.MySqlClient : Imports clsFuncionesLOG : Imports clsFuncionesC1 : Imports clsFuncionesUtiles : Imports clsConstantes
Imports Microsoft.VisualBasic.Compatibility.VB6

Public Class clsCliente
    Inherits clsADO

#Region "CAMPOS"
    '+-------------+---------------------+------+-----+---------+-------+
    '| Field       | Type                | Null | Key | Default | Extra |
    '+-------------+---------------------+------+-----+---------+-------+
    '| CODI        | int(11)             | NO   | PRI | 0       |       |
    '| CENTRO      | char(1)             | NO   | PRI | C       |       |
    '| NOM         | varchar(35)         | YES  |     | NULL    |       |
    '| DOM         | varchar(255)        | YES  |     | NULL    |       |
    '| CP          | varchar(5)          | YES  |     | NULL    |       |
    '| TEL         | varchar(20)         | YES  |     | NULL    |       |
    '| FAX         | varchar(20)         | YES  |     | NULL    |       |
    '| POB         | varchar(30)         | YES  |     | NULL    |       |
    '| PROV        | varchar(30)         | YES  |     | NULL    |       |
    '| NIF         | varchar(12)         | YES  |     | NULL    |       |
    '| NOTES       | text                | YES  |     | NULL    |       |
    '| CONTACTE    | varchar(40)         | YES  |     | NULL    |       |
    '| FORMA       | varchar(8)          | YES  |     | NULL    |       |
    '| DIA1        | int(11)             | YES  |     | NULL    |       |
    '| DIA2        | int(11)             | YES  |     | NULL    |       |
    '| DIA3        | int(11)             | YES  |     | NULL    |       |
    '| BANC        | varchar(4)          | YES  |     | NULL    |       |
    '| TEL2        | varchar(255)        | YES  |     | NULL    |       |
    '| PORTSDEBUTS | tinyint(1) unsigned | NO   |     | 0       |       |
    '| COFI        | varchar(4)          | YES  |     | NULL    |       |
    '| OFI         | varchar(30)         | YES  |     | NULL    |       |
    '| DC          | char(2)             | YES  |     | NULL    |       |
    '| CTA         | varchar(10)         | YES  |     | NULL    |       |
    '| IVA         | varchar(8)          | YES  |     | NULL    |       |
    '| TRANS       | int(11)             | YES  |     | NULL    |       |
    '| SUBCTA      | double              | YES  |     | NULL    |       |
    '| TRASPAS     | tinyint(1) unsigned | NO   |     | 0       |       |
    '| CARTERA     | tinyint(1) unsigned | NO   |     | 0       |       |
    '| REPRES      | int(11)             | YES  |     | NULL    |       |
    '| MONEDA      | char(1)             | YES  |     | NULL    |       |
    '| PORTSPAGATS | tinyint(1) unsigned | NO   |     | 0       |       |
    '| DTOFORMA    | double(15,2)        | YES  |     | NULL    |       |
    '| PAIS        | varchar(35)         | YES  |     | NULL    |       |
    '| WEB         | varchar(255)        | YES  |     | NULL    |       |
    '| EMAIL1      | varchar(255)        | YES  |     | NULL    |       |
    '| EMAIL2      | varchar(255)        | YES  |     | NULL    |       |
    '| BLOQUEADO   | boolean             | YES  |     | NULL    |       |
    '+-------------+---------------------+------+-----+---------+-------+
    '36 rows

    Private mCODI As Integer
    Public Property CODI() As Integer
        Get
            If PA() = -1 Then Exit Property
            Try
                mCODI = nzn(dvForm(PA).Row("CODI"), 0)
            Catch ex As Exception : End Try
            Return nzn(mCODI, 0)
        End Get
        Set(ByVal Value As Integer)
            If PA() = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(CODI, 0) Then
                mCODI = nzn(Value, 0)
                dvForm(PA).Row("CODI") = nzn(mCODI, 0) : guardarDV()
            End If
        End Set
    End Property

    Private mNOM As String
    Public Property NOM() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mNOM = general.nz(dvForm(PA).Row("NOM"), "")
            Catch ex As Exception : End Try
            Return general.nz(mNOM, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(NOM, "") Then
                mNOM = general.nz(Value, "")
                dvForm(PA).Row("NOM") = general.nz(mNOM, "") : guardarDV()
            End If
        End Set
    End Property

    Private mINCOTERM As String
    Public Property INCOTERM() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mINCOTERM = general.nz(dvForm(PA).Row("INCOTERM"), "")
            Catch ex As Exception : End Try
            Return general.nz(mINCOTERM, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(INCOTERM, "") Then
                mINCOTERM = general.nz(Value, "")
                dvForm(PA).Row("INCOTERM") = general.nz(mINCOTERM, "") : guardarDV()
            End If
        End Set
    End Property
    Private mDOM As String
    Public Property DOM() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mDOM = general.nz(dvForm(PA).Row("DOM"), "")
            Catch ex As Exception : End Try
            Return general.nz(mDOM, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(DOM, "") Then
                mDOM = general.nz(Value, "")
                dvForm(PA).Row("DOM") = general.nz(mDOM, "") : guardarDV()
            End If
        End Set
    End Property

    Private mCP As String
    Public Property CP() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mCP = general.nz(dvForm(PA).Row("CP"), "")
            Catch ex As Exception : End Try
            Return general.nz(mCP, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(CP, "") Then
                mCP = general.nz(Value, "")
                dvForm(PA).Row("CP") = general.nz(mCP, "") : guardarDV()
            End If
        End Set
    End Property

    Private mTEL As String
    Public Property TEL() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mTEL = general.nz(dvForm(PA).Row("TEL"), "")
            Catch ex As Exception : End Try
            Return general.nz(mTEL, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(TEL, "") Then
                mTEL = general.nz(Value, "")
                dvForm(PA).Row("TEL") = general.nz(mTEL, "") : guardarDV()
            End If
        End Set
    End Property

    Private mFAX As String
    Public Property FAX() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mFAX = general.nz(dvForm(PA).Row("FAX"), "")
            Catch ex As Exception : End Try
            Return general.nz(mFAX, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(FAX, "") Then
                mFAX = general.nz(Value, "")
                dvForm(PA).Row("FAX") = general.nz(mFAX, "") : guardarDV()
            End If
        End Set
    End Property

    Private mPOB As String
    Public Property POB() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mPOB = general.nz(dvForm(PA).Row("POB"), "")
            Catch ex As Exception : End Try
            Return general.nz(mPOB, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(POB, "") Then
                mPOB = general.nz(Value, "")
                dvForm(PA).Row("POB") = general.nz(mPOB, "") : guardarDV()
            End If
        End Set
    End Property

    Private mPROV As String
    Public Property PROV() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mPROV = general.nz(dvForm(PA).Row("PROV"), "")
            Catch ex As Exception : End Try
            Return general.nz(mPROV, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(PROV, "") Then
                mPROV = general.nz(Value, "")
                dvForm(PA).Row("PROV") = general.nz(mPROV, "") : guardarDV()
            End If
        End Set
    End Property

    Private mNIF As String
    Public Property NIF() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mNIF = general.nz(dvForm(PA).Row("NIF"), "")
            Catch ex As Exception : End Try
            Return general.nz(mNIF, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(NIF, "") Then
                mNIF = general.nz(Value, "")
                dvForm(PA).Row("NIF") = general.nz(mNIF, "") : guardarDV()
            End If
        End Set
    End Property

    Private mNOTES As String
    Public Property NOTES() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mNOTES = general.nz(dvForm(PA).Row("NOTES"), "")
            Catch ex As Exception : End Try
            Return general.nz(mNOTES, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(NOTES, "") Then
                mNOTES = general.nz(Value, "")
                dvForm(PA).Row("NOTES") = general.nz(mNOTES, "") : guardarDV()
            End If
        End Set
    End Property

    Private mCONTACTE As String
    Public Property CONTACTE() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mCONTACTE = general.nz(dvForm(PA).Row("CONTACTE"), "")
            Catch ex As Exception : End Try
            Return general.nz(mCONTACTE, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(CONTACTE, "") Then
                mCONTACTE = general.nz(Value, "")
                dvForm(PA).Row("CONTACTE") = general.nz(mCONTACTE, "") : guardarDV()
            End If
        End Set
    End Property

    Private mFORMA As String
    Public Property FORMA() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mFORMA = general.nz(dvForm(PA).Row("FORMA"), "")
            Catch ex As Exception : End Try
            Return general.nz(mFORMA, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            If esCodigoExistente(dtForpag, CCForpag, Value) Then
                If general.nz(Value, "") <> "" Then
                    mFORMA = general.nz(Value, "")
                    dvForm(PA).Row("NOMFORMA") = general.OBN(Value, dtForpag, CNForpag)
                    dvForm(PA).Row("FORMA") = general.nz(Value, "") : guardarDV()
                End If
            Else
                dvForm(PA).Row("FORMA") = ""

                dvForm(PA).Row("NOMFORMA") = "" : guardarDV()
                MessageBox.Show(rm.GetString("NOEXISTEFORPAG"))
            End If
        End Set
    End Property

    Private mNOMFORMA As String
    Public Property NOMFORMA() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mNOMFORMA = general.nz(dvForm(PA).Row("NOMFORMA"), "")
            Catch ex As Exception : End Try
            Return general.nz(mNOMFORMA, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            mNOMFORMA = general.nz(Value, "")
            If tabla.GetChanges Is Nothing Then
                dvForm(PA).Row("NOMFORMA") = general.nz(Value, "") : guardarDV()
                tabla.AcceptChanges()
            Else
                dvForm(PA).Row("NOMFORMA") = general.nz(Value, "") : guardarDV()
            End If
        End Set
    End Property

    Private mDIA1 As Integer
    Public Property DIA1() As Integer
        Get
            If PA() = -1 Then Exit Property
            Try
                mDIA1 = nzn(dvForm(PA).Row("DIA1"), 0)
            Catch ex As Exception : End Try
            Return nzn(mDIA1, 0)
        End Get
        Set(ByVal Value As Integer)
            If PA() = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(DIA1, 0) Then
                mDIA1 = nzn(Value, 0)
                dvForm(PA).Row("DIA1") = nzn(mDIA1, 0) : guardarDV()
            End If
        End Set
    End Property

    Private mDIA2 As Integer
    Public Property DIA2() As Integer
        Get
            If PA() = -1 Then Exit Property
            Try
                mDIA2 = nzn(dvForm(PA).Row("DIA2"), 0)
            Catch ex As Exception : End Try
            Return nzn(mDIA2, 0)
        End Get
        Set(ByVal Value As Integer)
            If PA() = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(DIA2, 0) Then
                mDIA2 = nzn(Value, 0)
                dvForm(PA).Row("DIA2") = nzn(mDIA2, 0) : guardarDV()
            End If
        End Set
    End Property

    Private mDIA3 As Integer
    Public Property DIA3() As Integer
        Get
            If PA() = -1 Then Exit Property
            Try
                mDIA3 = nzn(dvForm(PA).Row("DIA3"), 0)
            Catch ex As Exception : End Try
            Return nzn(mDIA3, 0)
        End Get
        Set(ByVal Value As Integer)
            If PA() = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(DIA3, 0) Then
                mDIA3 = nzn(Value, 0)
                dvForm(PA).Row("DIA3") = nzn(mDIA3, 0) : guardarDV()
            End If
        End Set
    End Property

    Private mBANC As String
    Public Property BANC() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mBANC = general.nz(dvForm(PA).Row("BANC"), "")
            Catch ex As Exception : End Try
            Return general.nz(mBANC, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            If esCodigoExistente(dtBancs, CCBancs, Value) Then
                If general.nz(Value, "") <> "" Then
                    mBANC = general.nz(Value, "")
                    dvForm(PA).Row("NOMBANC") = general.OBN(Value, dtBancs, CNBancs)
                    dvForm(PA).Row("BANC") = general.nz(Value, "") : guardarDV()
                End If
            Else
                dvForm(PA).Row("BANC") = ""

                dvForm(PA).Row("NOMBANC") = "" : guardarDV()
                MessageBox.Show(rm.GetString("NOEXISTEBANCS"))
            End If
        End Set
    End Property

    Private mNOMBANC As String
    Public Property NOMBANC() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mNOMBANC = general.nz(dvForm(PA).Row("NOMBANC"), "")
            Catch ex As Exception : End Try
            Return general.nz(mNOMBANC, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            mNOMBANC = general.nz(Value, "")
            If tabla.GetChanges Is Nothing Then
                dvForm(PA).Row("NOMBANC") = general.nz(Value, "") : guardarDV()
                tabla.AcceptChanges()
            Else
                dvForm(PA).Row("NOMBANC") = general.nz(Value, "") : guardarDV()
            End If
        End Set
    End Property

    Private mCOFI As String
    Public Property COFI() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mCOFI = general.nz(dvForm(PA).Row("COFI"), "")
            Catch ex As Exception : End Try
            Return general.nz(mCOFI, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(COFI, "") Then
                mCOFI = general.nz(Value, "")
                dvForm(PA).Row("COFI") = general.nz(mCOFI, "") : guardarDV()
            End If
        End Set
    End Property

    Private mOFI As String
    Public Property OFI() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mOFI = general.nz(dvForm(PA).Row("OFI"), "")
            Catch ex As Exception : End Try
            Return general.nz(mOFI, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(OFI, "") Then
                mOFI = general.nz(Value, "")
                dvForm(PA).Row("OFI") = general.nz(mOFI, "") : guardarDV()
            End If
        End Set
    End Property

    Private mDC As String
    Public Property DC() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mDC = general.nz(dvForm(PA).Row("DC"), "")
            Catch ex As Exception : End Try
            Return general.nz(mDC, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(DC, "") Then
                mDC = general.nz(Value, "")
                dvForm(PA).Row("DC") = general.nz(mDC, "") : guardarDV()
            End If
        End Set
    End Property

    Private mCTA As String
    Public Property CTA() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mCTA = general.nz(dvForm(PA).Row("CTA"), "")
            Catch ex As Exception : End Try
            Return general.nz(mCTA, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(CTA, "") Then
                mCTA = general.nz(Value, "")
                dvForm(PA).Row("CTA") = general.nz(mCTA, "") : guardarDV()
            End If
        End Set
    End Property

    Private mIVA As String
    Public Property IVA() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mIVA = general.nz(dvForm(PA).Row("IVA"), "")
            Catch ex As Exception : End Try
            Return general.nz(mIVA, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(IVA, "") Then
                mIVA = general.nz(Value, "")
                dvForm(PA).Row("IVA") = general.nz(mIVA, "") : guardarDV()
            End If
        End Set
    End Property

    Private mTRANS As Integer
    Public Property TRANS() As Integer
        Get
            If PA() = -1 Then Exit Property
            Try
                mTRANS = nzn(dvForm(PA).Row("TRANS"), 0)
            Catch ex As Exception : End Try
            Return nzn(mTRANS, 0)
        End Get
        Set(ByVal Value As Integer)
            If PA() = -1 Then Exit Property
            If esCodigoExistente(dtTrans, CCTrans, Value) Then
                If nzn(Value, 0) <> 0 Then
                    mTRANS = nzn(Value, 0)
                    dvForm(PA).Row("NOMTRANS") = general.OBN(Value, dtTrans, CNTrans)
                    dvForm(PA).Row("TRANS") = nzn(Value, 0) : guardarDV()
                End If
            Else
                dvForm(PA).Row("TRANS") = DBNull.Value

                dvForm(PA).Row("NOMTRANS") = "" : guardarDV()
                MessageBox.Show(rm.GetString("NOEXISTETRANS"))
            End If
        End Set
    End Property

    Private mNOMTRANS As String
    Public Property NOMTRANS() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mNOMTRANS = general.nz(dvForm(PA).Row("NOMTRANS"), "")
            Catch ex As Exception : End Try
            Return general.nz(mNOMTRANS, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            mNOMTRANS = general.nz(Value, "")
            If tabla.GetChanges Is Nothing Then
                dvForm(PA).Row("NOMTRANS") = general.nz(Value, "") : guardarDV()
                tabla.AcceptChanges()
            Else
                dvForm(PA).Row("NOMTRANS") = general.nz(Value, "") : guardarDV()
            End If
        End Set
    End Property

    Private mSUBCTA As Double
    Public Property SUBCTA() As Double
        Get
            If PA() = -1 Then Exit Property
            Try
                mSUBCTA = nzn(dvForm(PA).Row("SUBCTA"), 0)
            Catch ex As Exception : End Try
            Return nzn(mSUBCTA, 0)
        End Get
        Set(ByVal Value As Double)
            If PA() = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(SUBCTA, 0) Then
                mSUBCTA = nzn(Value, 0)
                dvForm(PA).Row("SUBCTA") = nzn(mSUBCTA, 0) : guardarDV()
            End If
        End Set
    End Property

    Private mTRASPAS As String
    Public Property TRASPAS() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mTRASPAS = general.nz(dvForm(PA).Row("TRASPAS"), "")
            Catch ex As Exception : End Try
            Return general.nz(mTRASPAS, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(TRASPAS, "") Then
                mTRASPAS = general.nz(Value, "")
                dvForm(PA).Row("TRASPAS") = general.nz(mTRASPAS, "") : guardarDV()
            End If
        End Set
    End Property

    Private mCARTERA As String
    Public Property CARTERA() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mCARTERA = general.nz(dvForm(PA).Row("CARTERA"), "")
            Catch ex As Exception : End Try
            Return general.nz(mCARTERA, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(CARTERA, "") Then
                mCARTERA = general.nz(Value, "")
                dvForm(PA).Row("CARTERA") = general.nz(mCARTERA, "") : guardarDV()
            End If
        End Set
    End Property

    Private mREPRES As Integer
    Public Property REPRES() As Integer
        Get
            If PA() = -1 Then Exit Property
            Try
                mREPRES = nzn(dvForm(PA).Row("REPRES"), 0)
            Catch ex As Exception : End Try
            Return nzn(mREPRES, 0)
        End Get
        Set(ByVal Value As Integer)
            If PA() = -1 Then Exit Property
            If esCodigoExistente(dtRepres, CCRepres, Value) Then
                If nzn(Value, 0) <> 0 Then
                    mREPRES = nzn(Value, Nothing)
                    dvForm(PA).Row("NOMREPRES") = general.OBN(Value, dtRepres, CNRepres)
                    dvForm(PA).Row("REPRES") = mREPRES : guardarDV()
                End If
            Else
                dvForm(PA).Row("REPRES") = DBNull.Value

                dvForm(PA).Row("NOMREPRES") = "" : guardarDV()
                MessageBox.Show(rm.GetString("NOEXISTEREPRES"))
            End If
        End Set
    End Property

    Private mNOMREPRES As String
    Public Property NOMREPRES() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mNOMREPRES = general.nz(dvForm(PA).Row("NOMREPRES"), "")
            Catch ex As Exception : End Try
            Return general.nz(mNOMREPRES, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            mNOMREPRES = general.nz(Value, "")
            If tabla.GetChanges Is Nothing Then
                dvForm(PA).Row("NOMREPRES") = general.nz(Value, "") : guardarDV()
                tabla.AcceptChanges()
            Else
                dvForm(PA).Row("NOMREPRES") = general.nz(Value, "") : guardarDV()
            End If
        End Set
    End Property

    Private mMONEDA As String
    Public Property MONEDA() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mMONEDA = general.nz(dvForm(PA).Row("MONEDA"), "")
            Catch ex As Exception : End Try
            Return general.nz(mMONEDA, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(MONEDA, "") Then
                mMONEDA = general.nz(Value, "")
                dvForm(PA).Row("MONEDA") = general.nz(mMONEDA, "") : guardarDV()
            End If
        End Set
    End Property

    Private mPORTSDEBUTS As Boolean
    Public Property PORTSDEBUTS() As Boolean
        Get
            If PA() = -1 Then Exit Property
            Try
                mPORTSDEBUTS = dvForm(PA).Row("PORTSDEBUTS")
            Catch ex As Exception : End Try
            Return mPORTSDEBUTS
        End Get
        Set(ByVal Value As Boolean)
            If PA() = -1 Then Exit Property
            If Value <> PORTSDEBUTS Then
                mPORTSDEBUTS = Value
                dvForm(PA).Row("PORTSDEBUTS") = mPORTSDEBUTS : guardarDV()
            End If
        End Set
    End Property

    Private mPORTSPAGATS As Boolean
    Public Property PORTSPAGATS() As Boolean
        Get
            If PA() = -1 Then Exit Property
            Try
                mPORTSPAGATS = dvForm(PA).Row("PORTSPAGATS")
            Catch ex As Exception : End Try
            Return mPORTSPAGATS
        End Get
        Set(ByVal Value As Boolean)
            If PA() = -1 Then Exit Property
            If Value <> PORTSPAGATS Then
                mPORTSPAGATS = Value
                dvForm(PA).Row("PORTSPAGATS") = mPORTSPAGATS : guardarDV()
            End If
        End Set
    End Property
    Private mDTOFORMA As Double
    Public Property DTOFORMA() As Double
        Get
            If PA() = -1 Then Exit Property
            Try
                mDTOFORMA = nzn(dvForm(PA).Row("DTOFORMA"), 0)
            Catch ex As Exception : End Try
            Return nzn(mDTOFORMA, 0)
        End Get
        Set(ByVal Value As Double)
            If PA() = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(DTOFORMA, 0) Then
                mDTOFORMA = nzn(Value, 0)
                dvForm(PA).Row("DTOFORMA") = nzn(mDTOFORMA, 0) : guardarDV()
            End If
        End Set
    End Property

    Private mPAIS As String
    Public Property PAIS() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mPAIS = general.nz(dvForm(PA).Row("PAIS"), "")
            Catch ex As Exception : End Try
            Return general.nz(mPAIS, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(PAIS, "") Then
                mPAIS = general.nz(Value, "")
                dvForm(PA).Row("PAIS") = general.nz(mPAIS, "") : guardarDV()
            End If
        End Set
    End Property

    Private mWEB As String
    Public Property WEB() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mWEB = general.nz(dvForm(PA).Row("WEB"), "")
            Catch ex As Exception : End Try
            Return general.nz(mWEB, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(WEB, "") Then
                mWEB = general.nz(Value, "")
                dvForm(PA).Row("WEB") = general.nz(mWEB, "") : guardarDV()
            End If
        End Set
    End Property

    Private mEMAIL1 As String
    Public Property EMAIL1() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mEMAIL1 = general.nz(dvForm(PA).Row("EMAIL1"), "")
            Catch ex As Exception : End Try
            Return general.nz(mEMAIL1, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(EMAIL1, "") Then
                mEMAIL1 = general.nz(Value, "")
                dvForm(PA).Row("EMAIL1") = general.nz(mEMAIL1, "") : guardarDV()
            End If
        End Set
    End Property

    Private mEMAIL2 As String
    Public Property EMAIL2() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mEMAIL2 = general.nz(dvForm(PA).Row("EMAIL2"), "")
            Catch ex As Exception : End Try
            Return general.nz(mEMAIL2, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(EMAIL2, "") Then
                mEMAIL2 = general.nz(Value, "")
                dvForm(PA).Row("EMAIL2") = general.nz(mEMAIL2, "") : guardarDV()
            End If
        End Set
    End Property

    Private mIBAN As String
    Public Property IBAN() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mIBAN = general.nz(dvForm(PA).Row("IBAN"), "")
            Catch ex As Exception : End Try
            Return general.nz(mIBAN, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(IBAN, "") Then
                mIBAN = general.nz(Value, "")
                dvForm(PA).Row("IBAN") = general.nz(mIBAN, "") : guardarDV()
            End If
        End Set
    End Property

    Private mSWIFT As String
    Public Property SWIFT() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mSWIFT = general.nz(dvForm(PA).Row("SWIFT"), "")
            Catch ex As Exception : End Try
            Return general.nz(mSWIFT, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(SWIFT, "") Then
                mSWIFT = general.nz(Value, "")
                dvForm(PA).Row("SWIFT") = general.nz(mSWIFT, "") : guardarDV()
            End If
        End Set

    End Property

    Private mBLOQUEADO As Boolean
    Public Property BLOQUEADO() As Boolean
        Get
            If PA() = -1 Then Exit Property
            Try
                mBLOQUEADO = nzn(dvForm(PA).Row("BLOQUEADO"), 0)
            Catch ex As Exception : End Try
            Return nzn(mBLOQUEADO, 0)
        End Get
        Set(ByVal Value As Boolean)
            If PA() = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(mBLOQUEADO, 0) Then
                mBLOQUEADO = nzn(Value, 0)
                dvForm(PA).Row("BLOQUEADO") = nzn(mBLOQUEADO, 0) : guardarDV()
            End If
        End Set
    End Property

#End Region

#Region "CAMPOS PROPIOS"

    Private mNOMTRANS2 As String
    Public Property NOMTRANS2() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mNOMTRANS2 = general.nz(dvForm(PA).Row("NOMTRANS2"), "")
            Catch ex As Exception : End Try
            Return general.nz(mNOMTRANS2, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            mNOMTRANS2 = general.nz(Value, "")
            If tabla.GetChanges Is Nothing Then
                'dvForm(PA).Row("NOMTRANS2") =general.nz(Value, "") : guardarDV()
                'tabla.AcceptChanges()
            Else
                'dvForm(PA).Row("NOMTRANS2") =general.nz(Value, "") : guardarDV()
            End If
        End Set
    End Property


#End Region
#Region "VARIABLES"

    Friend direcciones As clsDirecciones
    Friend dtTrans As New DataTable("TRANS")
    Friend dtIncoterms As New DataTable("INCOTERM")
    Friend dtForpag As New DataTable("FORPAG")
    Friend dtBancs As New DataTable("BANCS")
    Friend dtRepres As New DataTable("REPRES")

#End Region

    Public Sub New(ByVal tabla As DataTable, _
                ByVal centro As String, ByRef bindingcontext As BindingContext)

        MyBase.New(tabla, centro, bindingcontext)
        Dim sqlSel As String
        Dim UltimoCodigo As Integer
        Try
            sqlSinWhere = "SELECT clients.*, " & _
                            "filiales.DESCRI AS NOMCENTRO " & _
                            "FROM clients " & _
                            "LEFT JOIN filiales ON (filiales.CODI = clients.CENTRO) "

            Try
                UltimoCodigo = CargaParametro("cliente")
                numeroRegistroActual = ObtenerNumeroRegistro(UltimoCodigo)
                If numeroRegistroActual = -1 Then
                    numeroRegistroActual = 0
                    sqlSel = sqlSinWhere & _
                            " WHERE clients.CENTRO = """ & centro & """ ORDER BY clients.CODI " & _
                            " LIMIT 1"
                Else
                    sqlSel = sqlSinWhere & _
                              " WHERE clients.CENTRO = """ & centro & """ AND CLIENTS.CODI = """ & UltimoCodigo & """ ORDER BY clients.CODI " & _
                              " LIMIT 1"
                End If

            Catch ex As Exception
                sqlSel = sqlSinWhere & _
                            " WHERE clients.CENTRO = """ & centro & """ ORDER BY clients.CODI " & _
                            " LIMIT 1"
            End Try

            cmdSel.CommandText = sqlSel
            da.SelectCommand = cmdSel
            da.Fill(tabla)
            CargarIdentificadores()
            CargarTablas()
            IniciarDirecciones()

            PonerNombres()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub CargarTablas()
        CargaTabla(tablaTransportistas, "CODI", "NOM", dtTrans, True)
        CargaTabla(tablaRepresentantes, "CODI", "NOM", dtRepres, True)
        CargaTabla(tablaFormaPago, "CODI", "DESCRIPCIO", dtForpag, True)
        CargaTabla(tablaBancos, "CODI", "DESCRIPCIO", dtBancs, True)
        CargaTabla("incoterm", "NOMBRE", "DESCRI", dtIncoterms, True)
    End Sub
    Public Overrides Sub CargarIdentificadores()
        Try
            CargaTabla(tablaClientes, CCClients, CNClients, dtIdentificadores, True)
            dtIdentificadores.Columns(CCClients).Caption = rm.GetString("CODIGO")
            dtIdentificadores.Columns(CNClients).Caption = rm.GetString("NOM")
            dvIdentificadores.Sort = CNClients
            dvIdentificadores.RowFilter = "CENTRO = '" & centro & "' "

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Sub

#Region "DESPLAZAMIENTO"

    Public Overloads Sub SiguienteReg()
        Try
            MyBase.SiguienteReg()
            MoverActual()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Public Overloads Sub AnteriorReg()
        Try
            MyBase.AnteriorReg()
            MoverActual()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Public Overloads Sub UltimoReg()
        Try
            MyBase.UltimoReg()
            MoverActual()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Public Overloads Sub PrimeroReg()
        Try
            MyBase.PrimeroReg()
            MoverActual()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Public Overloads Sub CambiarAReg(ByVal id As String, ByVal accion As Integer)
        Dim sql As String
        Try
            sql = "SELECT CODI FROM CLIENTS WHERE (CENTRO = """ & centro & """ AND NOM = """ & id & """)"
            MyBase.CambiarAReg(id, sql, accion)
            MoverActual()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#End Region

    Public Overrides Sub ActualizarOrigen(Optional ByVal nocerrar As Boolean = False, Optional ByVal hackDetalle As Boolean = False)
        Try
            MyBase.ActualizarOrigen(True)
            direcciones.ActualizarOrigen(True, True)
            MoverActual()
            CCN()

        Catch ex As Exception
            Throw ex
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Public Overloads Sub NuevoRegistro()

        Try
            NuevoCliente()
            'MyBase.NuevoRegistro()
            MoverActual()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

    Protected Friend Function ObtenerNuevoID() As Integer
        Dim i As Integer = 2000
        Dim idx As Integer
        Dim cmdSel As MySqlCommand
        Try
            Do
                i = i + 1000
                cmdSel = New MySqlCommand("SELECT CODI +1 FROM " & tabla.TableName & " WHERE CODI < " & i & " ORDER BY CODI DESC LIMIT 1", cnn)
                ACN()
                idx = CInt(cmdSel.ExecuteScalar)

                CCN()
            Loop Until idx <> i
            Return idx
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Function

    Public Sub NuevoCliente()
        Dim drNew As DataRow
        Dim cm As CurrencyManager
        Try
            ActualizarOrigen()
            tabla.Clear()
            cm = CType(bc(dvForm), CurrencyManager)
            drNew = tabla.NewRow()

            If tabla.Columns("CODI").DataType.GetTypeCode(tabla.Columns("CODI").DataType) = TypeCode.String Then
                drNew.Item("CODI") = ""
            Else
                drNew.Item("CODI") = ObtenerNuevoID()
            End If
            drNew.Item("CENTRO") = centro
            tabla.Rows.Add(drNew)
            cm.Position = 1
            numeroRegistros = numeroRegistros + 1

            Try
                guardarDV()
            Catch ex As Exception
            End Try

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

    Private Sub MoverActual()
        CanviarDirecciones()
        PonerNombres()
    End Sub
    Private Sub CanviarDirecciones()
        Try
            direcciones.CambioDetalle(centro, Me)

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub IniciarDirecciones()
        Try
            direcciones = New clsDirecciones(New aura2k3.ds11.adresDataTable, centro, bc, Me)

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Friend Overrides Sub PonerNombres()
        Dim aceptarCambiosAlFinal As Boolean = False
        Try
            If tabla.GetChanges Is Nothing Then aceptarCambiosAlFinal = True
            NOMBANC = general.OBN(BANC, dtBancs, "DESCRIPCIO")
            NOMREPRES = general.OBN(REPRES, dtRepres)
            NOMTRANS = general.OBN(TRANS, dtTrans)
            NOMFORMA = general.OBN(FORMA, dtForpag, "DESCRIPCIO")

            If aceptarCambiosAlFinal AndAlso (Not tabla.GetChanges Is Nothing) Then tabla.AcceptChanges()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False
        End Try
    End Sub

#Region "OVERRIDES"

    Friend Overrides Function TieneCambios() As Boolean
        Try
            guardarDV()
            If Not tabla.GetChanges Is Nothing OrElse direcciones.TieneCambios Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Function
    Public Overloads Sub borrar()
        Try
            ACN()
            MyBase.borrar()
            direcciones.BorrarActualDVDetalle(True)
            MoverActual()
            CCN()

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
            Return " ORDER BY CODI "
            'Return " ORDER BY TEMPORADA, CLIENT, SERIE, CODI "
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Function
    Friend Overrides Function ObtenerNumeroRegistro(ByVal id As Object) As Integer
        If id Is Nothing Then
            id = CODI
        End If

        Dim cmd As New MySqlCommand(" SELECT " & _
          " (SELECT COUNT(*) " & _
          " FROM " & tabla.TableName & " AS M2 WHERE " & _
          " M2.CODI < M1.CODI AND  " & WCNoTabla() & " ) AS rownum FROM " & tabla.TableName & " AS M1 " & _
          " WHERE CODI = """ & id & """ AND " & WCNoTabla() & GenOrder(), cnn)
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

#End Region

#Region "IMPRESION"

    Private Function PonerEtiqueta(ByVal xls As ExcelDirecto.ExcelDirecto, ByVal bulto As Double, ByVal peso As Double) As Integer
        Try

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Function
    Private Function PonerDetalle(ByVal xls As ExcelDirecto.ExcelDirecto, ByVal bultos As Double, ByVal peso As Double) As Integer
        Dim i, temp As Integer
        Dim hojaNueva As Boolean = False
        Try
            For i = 0 To bultos - 1
                temp = PonerEtiqueta(xls, i, peso)
            Next
            Return temp

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Function
    Private Function CrearTablaEtiquetasEnvio(ByVal bultos As Double, ByVal peso As Double, ByVal centro As Char, ByVal aDireccionEnvio As Boolean) As DataTable
        Dim dt As New DataTable("ETIENVIO")
        Dim i As Integer
        Dim dr As DataRow
        Try
            'Ahora con direcciones de envio
            dt.Columns.Add(New DataColumn("CONSIGNATARIO"))
            dt.Columns.Add(New DataColumn("DOMICILIO"))
            dt.Columns.Add(New DataColumn("POB"))
            dt.Columns.Add(New DataColumn("CP"))
            dt.Columns.Add(New DataColumn("PROV"))
            dt.Columns.Add(New DataColumn("EXPPOR"))
            dt.Columns.Add(New DataColumn("NBULTOS"))
            dt.Columns.Add(New DataColumn("PAIS"))
            dt.Columns.Add(New DataColumn("PESO"))
            dt.Columns.Add(New DataColumn("BULTON"))
            dt.Columns("NBULTOS").DefaultValue = bultos
            If NOMTRANS2 <> "" Then
                dt.Columns("EXPPOR").DefaultValue = NOMTRANS2
            Else
                dt.Columns("EXPPOR").DefaultValue = NOMTRANS
            End If
            dt.Columns("PESO").DefaultValue = peso

            If aDireccionEnvio Then
                dt.Columns("CONSIGNATARIO").DefaultValue = direcciones.NOM
                dt.Columns("DOMICILIO").DefaultValue = direcciones.DOM
                dt.Columns("PAIS").DefaultValue = direcciones.PAIS
                dt.Columns("POB").DefaultValue = direcciones.POB
                dt.Columns("CP").DefaultValue = direcciones.CP
                dt.Columns("PROV").DefaultValue = direcciones.PROV
            Else
                dt.Columns("CONSIGNATARIO").DefaultValue = NOM
                dt.Columns("PAIS").DefaultValue = PAIS
                dt.Columns("DOMICILIO").DefaultValue = DOM
                dt.Columns("POB").DefaultValue = POB
                dt.Columns("CP").DefaultValue = CP
                dt.Columns("PROV").DefaultValue = PROV
            End If

            For i = 1 To bultos
                dr = dt.NewRow
                dr("BULTON") = i
                dt.Rows.Add(dr)
            Next

            Return dt

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Function

    Public Function ImprimirEtiquetasEnvio(ByVal bultos As Double, ByVal peso As Double, ByVal centro As Char, Optional ByVal aDireccionEnvio As Boolean = False) As System.Drawing.Printing.PrintDocument
        Try
            Dim c1r As New C1.C1Report.C1Report
            Select Case centro
                Case "C"
                    c1r.Load(CurDir() & "\informes\informes.xml", "ETIQUETASENVIO")
                Case "O"
                    c1r.Load(CurDir() & "\informes\informes.xml", "ETIQUETASENVIOCYCO")
                Case "I"
                    c1r.Load(CurDir() & "\informes\informes.xml", "ETIQUETASENVIOINFINIPUNT")
                Case "M"
                    c1r.Load(CurDir() & "\informes\informes.xml", "ETIQUETASENVIOCOMPLETEX")
            End Select

            c1r.DataSource.Recordset = CrearTablaEtiquetasEnvio(bultos, peso, centro, aDireccionEnvio)
            'c1r.RenderToFile("tempET", C1.Win.C1Report.FileFormatEnum.Text)

            Return c1r.Document

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Function

#End Region

End Class
