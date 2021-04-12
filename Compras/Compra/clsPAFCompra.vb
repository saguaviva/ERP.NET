Imports MySql.Data.MySqlClient : Imports clsFuncionesLOG : Imports clsFuncionesC1 : Imports clsFuncionesUtiles : Imports clsConstantes : Imports clsOtrasFunciones
Imports Excel = Microsoft.Office.Interop.Excel

Public Class clsPAFCompra
    Inherits clsADO

#Region "VARIABLES"

    Friend lineasCompra As clsLineasCompra

    Friend IDProve As Integer = -1
    Public mProveSel As Integer = -1

    Friend dtForpag As New DataTable("FORPAG")
    Friend dtBancs As New DataTable("BANCS")
    Friend dtTallers As New DataTable("TALLERS")
    Friend dtProve As New DataTable("PROVE")
    Friend dtProveConPAF As DataTable
    Friend dtOrdeneFabComplementos As New DataTable

    Private documento as string
    Private tipo as string

#End Region

#Region "CAMPOS"

    '+----------+-----------------------+------+-----+---------+-------+
    '| Field    | Type                  | Null | Key | Default | Extra |
    '+----------+-----------------------+------+-----+---------+-------+
    '| TIPUS    | char(1)               | NO   | PRI |         |       |
    '| DOCUMENT | char(1)               | NO   | PRI |         |       |
    '| FRA      | int(11)               | NO   | PRI | 0       |       |
    '| CLIENT   | int(11)               | YES  |     | NULL    |       |
    '| DATA     | date                  | YES  |     | NULL    |       |
    '| BASE1    | double                | YES  |     | NULL    |       |
    '| P_IVA1   | double                | YES  |     | NULL    |       |
    '| IVA1     | double                | YES  |     | NULL    |       |
    '| TOTAL    | double                | YES  |     | NULL    |       |
    '| P_DTE    | double                | YES  |     | NULL    |       |
    '| DTE1     | double                | YES  |     | NULL    |       |
    '| BRUT1    | double                | YES  |     | NULL    |       |
    '| TRASPAS  | char(1)               | YES  |     | NULL    |       |
    '| P_RE1    | double                | YES  |     | NULL    |       |
    '| RE1      | double                | YES  |     | NULL    |       |
    '| VENCIM   | char(1)               | YES  |     | NULL    |       |
    '| TRASPASF | tinyint(1)            | YES  |     | NULL    |       |
    '| OBSERV   | mediumtext            | YES  |     | NULL    |       |
    '| REPRES   | int(11)               | YES  |     | NULL    |       |
    '| COMIS    | double                | YES  |     | NULL    |       |
    '| CENTRO   | char(1)               | NO   | PRI | C       |       |
    '| ORDRE    | double(15,0) unsigned | YES  |     | NULL    |       |
    '| PREU     | double(15,3)          | YES  |     | NULL    |       |
    '| ALBCLI   | varchar(255)          | YES  |     | NULL    |       |
    '+----------+-----------------------+------+-----+---------+-------+


    Private mTIPUS As String
    Public Property TIPUS() As String
        Get
            If PA() = -1 Then Exit Property
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

    Private mORDENFABCOMP As Integer
    Public Property ORDENFABCOMP() As Integer
        Get
            If PA() = -1 Then Exit Property
            Try
                mORDENFABCOMP = nzn(dvForm(PA).Row("ORDENFABCOMP"), 0)
            Catch ex As Exception : End Try
            Return nzn(mFRA, 0)
        End Get
        Set(ByVal Value As Integer)
            If PA() = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(ORDENFABCOMP, 0) Then
                mORDENFABCOMP = nzn(Value, 0)
                dvForm(PA).Row("ORDENFABCOMP") = nzn(mORDENFABCOMP, 0) : guardarDV()
            End If
        End Set
    End Property
    Private mCOMANDAMOSTRA As Integer
    Public Property COMANDAMOSTRA() As Integer
        Get
            If PA() = -1 Then Exit Property
            Try
                mCOMANDAMOSTRA = nzn(dvForm(PA).Row("COMANDAMOSTRA"), 0)
            Catch ex As Exception : End Try
            Return nzn(mFRA, 0)
        End Get
        Set(ByVal Value As Integer)
            If PA() = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(COMANDAMOSTRA, 0) Then
                mCOMANDAMOSTRA = nzn(Value, 0)
                dvForm(PA).Row("COMANDAMOSTRA") = nzn(mCOMANDAMOSTRA, 0) : guardarDV()
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

    Private mPROVE As Integer
    Public Property PROVE() As Integer
        Get
            If PA() = -1 Then Exit Property
            Try
                mPROVE = nzn(dvForm(PA).Row("PROVE"), 0)
            Catch ex As Exception : End Try
            Return nzn(mPROVE, 0)
        End Get
        Set(ByVal Value As Integer)
            If PA() = -1 Then Exit Property
            If esCodigoExistente(dtProve, CCProve, Value) Then
                If nzn(Value, 0) <> 0 Then
                    mPROVE = nzn(Value, 0)
                    dvForm(PA).Row("NOMPROVE") = general.OBN(Value, dtProve, CNProve)
                    dvForm(PA).Row(tablaProveedores) = nzn(Value, 0) : guardarDV()
                End If
            Else
                dvForm(PA).Row("PROVE") = 0

                dvForm(PA).Row("NOMPROVE") = "" : guardarDV()
                MessageBox.Show(rm.GetString("NOEXISTEPROVE"))
            End If
        End Set
    End Property

    Private mNOMPROVE As String
    Public Property NOMPROVE() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mNOMPROVE = general.nz(dvForm(PA).Row("NOMPROVE"), "")
            Catch ex As Exception : End Try
            Return general.nz(mNOMPROVE, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            mNOMPROVE = general.nz(Value, "")
            If tabla.GetChanges Is Nothing Then
                dvForm(PA).Row("NOMPROVE") = general.nz(Value, "") : guardarDV()
                tabla.AcceptChanges()
            Else
                dvForm(PA).Row("NOMPROVE") = general.nz(Value, "") : guardarDV()
            End If
        End Set
    End Property
    Private mTALLER As Integer
    Public Property TALLER() As Integer
        Get
            If PA() = -1 Then Exit Property
            Try
                mTALLER = nzn(dvForm(PA).Row("TALLER"), 0)
            Catch ex As Exception : End Try
            Return nzn(mTALLER, 0)
        End Get
        Set(ByVal Value As Integer)
            If PA() = -1 Then Exit Property
            If esCodigoExistente(dtTallers, CCTallers, Value) Then
                If nzn(Value, 0) <> 0 Then
                    mTALLER = nzn(Value, 0)
                    dvForm(PA).Row("NOMTALLER") = general.OBN(Value, dtTallers, CNTallers)
                    dvForm(PA).Row("TALLER") = nzn(Value, 0) : guardarDV()
                End If
            Else
                dvForm(PA).Row("TALLER") = 0

                dvForm(PA).Row("NOMTALLER") = "" : guardarDV()
                MessageBox.Show(rm.GetString("NOEXISTETALLER"))
            End If
        End Set
    End Property

    Private mNOMTALLER As String
    Public Property NOMTALLER() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mNOMTALLER = general.nz(dvForm(PA).Row("NOMTALLER"), "")
            Catch ex As Exception : End Try
            Return general.nz(mNOMTALLER, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            mNOMTALLER = general.nz(Value, "")
            If tabla.GetChanges Is Nothing Then
                dvForm(PA).Row("NOMTALLER") = general.nz(Value, "") : guardarDV()
                tabla.AcceptChanges()
            Else
                dvForm(PA).Row("NOMTALLER") = general.nz(Value, "") : guardarDV()
            End If
        End Set
    End Property
    Private mNRO As Integer
    Public Property NRO() As Integer
        Get
            If PA() = -1 Then Exit Property
            Try
                mNRO = nzn(dvForm(PA).Row("NRO"), 0)
            Catch ex As Exception : End Try
            Return nzn(mNRO, 0)
        End Get
        Set(ByVal Value As Integer)
            If PA() = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(NRO, 0) Then
                mNRO = nzn(Value, 0)
                dvForm(PA).Row("NRO") = nzn(mNRO, 0) : guardarDV()
            End If
        End Set
    End Property

    Private mDATA As Date
    Public Property DATA() As Date
        Get
            If PA() = -1 Then Exit Property
            Try
                mDATA = dvForm(PA).Row("DATA")
            Catch ex As Exception : End Try
            Return mDATA
        End Get
        Set(ByVal Value As Date)
            If PA() = -1 Then Exit Property
            If Value <> DATA Then
                mDATA = Value
                dvForm(PA).Row("DATA") = mDATA : guardarDV()
            End If
        End Set
    End Property

    Private mDATAENTREGA As Date
    Public Property DATAENTREGA() As Date
        Get
            If PA() = -1 Then Exit Property
            Try
                mDATAENTREGA = dvForm(PA).Row("DATAENTREGA")
            Catch ex As Exception : End Try
            Return mDATAENTREGA
        End Get
        Set(ByVal Value As Date)
            If PA() = -1 Then Exit Property
            If Value <> DATAENTREGA Then
                mDATAENTREGA = Value
                dvForm(PA).Row("DATAENTREGA") = mDATAENTREGA : guardarDV()
            End If
        End Set
    End Property

    Private mBASE1 As Double
    Public Property BASE1() As Double
        Get
            If PA() = -1 Then Exit Property
            Try
                mBASE1 = nzn(dvForm(PA).Row("BASE1"), 0)
            Catch ex As Exception : End Try
            Return nzn(mBASE1, 0)
        End Get
        Set(ByVal Value As Double)
            If PA() = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(BASE1, 0) Then
                mBASE1 = nzn(Value, 0)
                dvForm(PA).Row("BASE1") = nzn(mBASE1, 0) : guardarDV()
            End If
        End Set
    End Property

    Private mP_IVA1 As Double
    Public Property P_IVA1() As Double
        Get
            If PA() = -1 Then Exit Property
            Try
                mP_IVA1 = nzn(dvForm(PA).Row("P_IVA1"), 0)
            Catch ex As Exception : End Try
            Return nzn(mP_IVA1, 0)
        End Get
        Set(ByVal Value As Double)
            If PA() = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(P_IVA1, 0) Then
                mP_IVA1 = nzn(Value, 0)
                dvForm(PA).Row("P_IVA1") = nzn(mP_IVA1, 0) : guardarDV()
            End If
        End Set
    End Property

    Private mIVA1 As Double
    Public Property IVA1() As Double
        Get
            If PA() = -1 Then Exit Property
            Try
                mIVA1 = nzn(dvForm(PA).Row("IVA1"), 0)
            Catch ex As Exception : End Try
            Return nzn(mIVA1, 0)
        End Get
        Set(ByVal Value As Double)
            If PA() = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(IVA1, 0) Then
                mIVA1 = nzn(Value, 0)
                dvForm(PA).Row("IVA1") = nzn(mIVA1, 0) : guardarDV()
            End If
        End Set
    End Property



    Private mTOTAL As Double
    Public Property TOTAL() As Double
        Get
            If PA() = -1 Then Exit Property
            Try
                mTOTAL = nzn(dvForm(PA).Row("TOTAL"), 0)
            Catch ex As Exception : End Try
            Return nzn(mTOTAL, 0)
        End Get
        Set(ByVal Value As Double)
            If PA() = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(TOTAL, 0) Then
                mTOTAL = nzn(Value, 0)
                dvForm(PA).Row("TOTAL") = nzn(mTOTAL, 0) : guardarDV()
            End If
        End Set
    End Property

    Private mP_DTE As Double
    Public Property P_DTE() As Double
        Get
            If PA() = -1 Then Exit Property
            Try
                mP_DTE = nzn(dvForm(PA).Row("P_DTE"), 0)
            Catch ex As Exception : End Try
            Return nzn(mP_DTE, 0)
        End Get
        Set(ByVal Value As Double)
            If PA() = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(P_DTE, 0) Then
                mP_DTE = nzn(Value, 0)
                dvForm(PA).Row("P_DTE") = nzn(mP_DTE, 0) : guardarDV()
            End If
        End Set
    End Property

    Private mDTE1 As Double
    Public Property DTE1() As Double
        Get
            If PA() = -1 Then Exit Property
            Try
                mDTE1 = nzn(dvForm(PA).Row("DTE1"), 0)
            Catch ex As Exception : End Try
            Return nzn(mDTE1, 0)
        End Get
        Set(ByVal Value As Double)
            If PA() = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(DTE1, 0) Then
                mDTE1 = nzn(Value, 0)
                dvForm(PA).Row("DTE1") = nzn(mDTE1, 0) : guardarDV()
            End If
        End Set
    End Property

    Private mBRUT1 As Double
    Public Property BRUT1() As Double
        Get
            If PA() = -1 Then Exit Property
            Try
                mBRUT1 = nzn(dvForm(PA).Row("BRUT1"), 0)
            Catch ex As Exception : End Try
            Return nzn(mBRUT1, 0)
        End Get
        Set(ByVal Value As Double)
            If PA() = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(BRUT1, 0) Then
                mBRUT1 = nzn(Value, 0)
                dvForm(PA).Row("BRUT1") = nzn(mBRUT1, 0) : guardarDV()
            End If
        End Set
    End Property

    Private mTRASPAS As Boolean
    Public Property TRASPAS() As Boolean
        Get
            If PA() = -1 Then Exit Property
            Try
                mTRASPAS = dvForm(PA).Row("TRASPAS")
            Catch ex As Exception : End Try
            Return mTRASPAS
        End Get
        Set(ByVal Value As Boolean)
            If PA() = -1 Then Exit Property
            If Value <> TRASPAS Then
                mTRASPAS = Value
                dvForm(PA).Row("TRASPAS") = mTRASPAS : guardarDV()
            End If
        End Set
    End Property
    Private mP_RE1 As Double
    Public Property P_RE1() As Double
        Get
            If PA() = -1 Then Exit Property
            Try
                mP_RE1 = nzn(dvForm(PA).Row("P_RE1"), 0)
            Catch ex As Exception : End Try
            Return nzn(mP_RE1, 0)
        End Get
        Set(ByVal Value As Double)
            If PA() = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(P_RE1, 0) Then
                mP_RE1 = nzn(Value, 0)
                dvForm(PA).Row("P_RE1") = nzn(mP_RE1, 0) : guardarDV()
            End If
        End Set
    End Property

    Private mRE1 As Double
    Public Property RE1() As Double
        Get
            If PA() = -1 Then Exit Property
            Try
                mRE1 = nzn(dvForm(PA).Row("RE1"), 0)
            Catch ex As Exception : End Try
            Return nzn(mRE1, 0)
        End Get
        Set(ByVal Value As Double)
            If PA() = -1 Then Exit Property
            If nzn(Value, 0) <> nzn(RE1, 0) Then
                mRE1 = nzn(Value, 0)
                dvForm(PA).Row("RE1") = nzn(mRE1, 0) : guardarDV()
            End If
        End Set
    End Property

    Private mVENCIM As String
    Public Property VENCIM() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mVENCIM = general.nz(dvForm(PA).Row("VENCIM"), "")
            Catch ex As Exception : End Try
            Return general.nz(mVENCIM, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(VENCIM, "") Then
                mVENCIM = general.nz(Value, "")
                dvForm(PA).Row("VENCIM") = general.nz(mVENCIM, "") : guardarDV()
            End If
        End Set
    End Property

    Private mTRASPASF As Boolean
    Public Property TRASPASF() As Boolean
        Get
            If PA() = -1 Then Exit Property
            Try
                mTRASPASF = dvForm(PA).Row("TRASPASF")
            Catch ex As Exception : End Try
            Return mTRASPASF
        End Get
        Set(ByVal Value As Boolean)
            If PA() = -1 Then Exit Property
            If Value <> TRASPASF Then
                mTRASPASF = Value
                dvForm(PA).Row("TRASPASF") = mTRASPASF : guardarDV()
            End If
        End Set
    End Property

    Private mOBSERV As String
    Public Property OBSERV() As String
        Get
            If PA() = -1 Then Exit Property
            Try
                mOBSERV = general.nz(dvForm(PA).Row("OBSERV"), "")
            Catch ex As Exception : End Try
            Return general.nz(mOBSERV, "")
        End Get
        Set(ByVal Value As String)
            If PA() = -1 Then Exit Property
            If general.nz(Value, "") <> general.nz(OBSERV, "") Then
                mOBSERV = general.nz(Value, "")
                dvForm(PA).Row("OBSERV") = general.nz(mOBSERV, "") : guardarDV()
            End If
        End Set
    End Property

#End Region

#Region "CAMPOS PROPIOS"

    Private mDIA1 As Integer
    Public Property DIA1() As Integer
        Get
            Try
                mDIA1 = general.nz(dvForm(PA).Row("DIA1"), "")
            Catch ex As Exception : End Try
            Return general.nz(mDIA1, "")
        End Get
        Set(ByVal Value As Integer)
            mDIA1 = general.nz(Value, "")
            If tabla.GetChanges Is Nothing Then
                dvForm(PA).Row("DIA1") = general.nz(Value, "") : guardarDV()
                tabla.AcceptChanges()
            Else
                dvForm(PA).Row("DIA1") = general.nz(Value, "") : guardarDV()
            End If
        End Set
    End Property

    Private mDIA2 As Integer
    Public Property DIA2() As Integer
        Get
            Try
                mDIA2 = general.nz(dvForm(PA).Row("DIA2"), "")
            Catch ex As Exception : End Try
            Return general.nz(mDIA2, "")
        End Get
        Set(ByVal Value As Integer)
            mDIA2 = general.nz(Value, "")
            If tabla.GetChanges Is Nothing Then
                dvForm(PA).Row("DIA2") = general.nz(Value, "") : guardarDV()
                tabla.AcceptChanges()
            Else
                dvForm(PA).Row("DIA2") = general.nz(Value, "") : guardarDV()
            End If
        End Set
    End Property

    Private mDIA3 As Integer
    Public Property DIA3() As Integer
        Get
            Try
                mDIA3 = general.nz(dvForm(PA).Row("DIA3"), "")
            Catch ex As Exception : End Try
            Return general.nz(mDIA3, "")
        End Get
        Set(ByVal Value As Integer)
            mDIA3 = general.nz(Value, "")
            If tabla.GetChanges Is Nothing Then
                dvForm(PA).Row("DIA3") = general.nz(Value, "") : guardarDV()
                tabla.AcceptChanges()
            Else
                dvForm(PA).Row("DIA3") = general.nz(Value, "") : guardarDV()
            End If
        End Set
    End Property

    Private mFORMA As String
    Public Property FORMA() As String
        Get
            Try
                mFORMA = general.nz(dvForm(PA).Row("FORMA"), "")
            Catch ex As Exception : End Try
            Return general.nz(mFORMA, "")
        End Get
        Set(ByVal Value As String)
            mFORMA = general.nz(Value, "")
            If tabla.GetChanges Is Nothing Then
                dvForm(PA).Row("FORMA") = general.nz(Value, "") : guardarDV()
                tabla.AcceptChanges()
            Else
                dvForm(PA).Row("FORMA") = general.nz(Value, "") : guardarDV()
            End If
        End Set
    End Property

    Private mcta As String
    Public Property cta() As String
        Get
            Try
                mcta = general.nz(dvForm(PA).Row("cta"), "")
            Catch ex As Exception : End Try
            Return general.nz(mcta, "")
        End Get
        Set(ByVal Value As String)
            mcta = general.nz(Value, "")
            If tabla.GetChanges Is Nothing Then
                dvForm(PA).Row("cta") = general.nz(Value, "") : guardarDV()
                tabla.AcceptChanges()
            Else
                dvForm(PA).Row("cta") = general.nz(Value, "") : guardarDV()
            End If
        End Set
    End Property

    Private mREPRECLIENT As String
    Public Property REPRECLIENT() As String
        Get
            Try
                mREPRECLIENT = general.nz(dvForm(PA).Row("REPRECLIENT"), "")
            Catch ex As Exception : End Try
            Return general.nz(mREPRECLIENT, "")
        End Get
        Set(ByVal Value As String)
            mREPRECLIENT = general.nz(Value, "")
            If tabla.GetChanges Is Nothing Then
                dvForm(PA).Row("REPRECLIENT") = general.nz(Value, "") : guardarDV()
                tabla.AcceptChanges()
            Else
                dvForm(PA).Row("REPRECLIENT") = general.nz(Value, "") : guardarDV()
            End If
        End Set
    End Property

    Private mBANC As String
    Public Property BANC() As String
        Get
            Try
                mBANC = general.nz(dvForm(PA).Row("BANC"), "")
            Catch ex As Exception : End Try
            Return general.nz(mBANC, "")
        End Get
        Set(ByVal Value As String)
            mBANC = general.nz(Value, "")
            If tabla.GetChanges Is Nothing Then
                dvForm(PA).Row("BANC") = general.nz(Value, "") : guardarDV()
                tabla.AcceptChanges()
            Else
                dvForm(PA).Row("BANC") = general.nz(Value, "") : guardarDV()
            End If
        End Set
    End Property

    Private mCOFI As String
    Public Property COFI() As String
        Get
            Try
                mCOFI = general.nz(dvForm(PA).Row("COFI"), "")
            Catch ex As Exception : End Try
            Return general.nz(mCOFI, "")
        End Get
        Set(ByVal Value As String)
            mCOFI = general.nz(Value, "")
            If tabla.GetChanges Is Nothing Then
                dvForm(PA).Row("COFI") = general.nz(Value, "") : guardarDV()
                tabla.AcceptChanges()
            Else
                dvForm(PA).Row("COFI") = general.nz(Value, "") : guardarDV()
            End If
        End Set
    End Property

    Private mOFI As String
    Public Property OFI() As String
        Get
            Try
                mOFI = general.nz(dvForm(PA).Row("OFI"), "")
            Catch ex As Exception : End Try
            Return general.nz(mOFI, "")
        End Get
        Set(ByVal Value As String)
            mOFI = general.nz(Value, "")
            If tabla.GetChanges Is Nothing Then
                dvForm(PA).Row("OFI") = general.nz(Value, "") : guardarDV()
                tabla.AcceptChanges()
            Else
                dvForm(PA).Row("OFI") = general.nz(Value, "") : guardarDV()
            End If
        End Set
    End Property

    Private mDC As String
    Public Property DC() As String
        Get
            Try
                mDC = general.nz(dvForm(PA).Row("DC"), "")
            Catch ex As Exception : End Try
            Return general.nz(mDC, "")
        End Get
        Set(ByVal Value As String)
            mDC = general.nz(Value, "")
            If tabla.GetChanges Is Nothing Then
                dvForm(PA).Row("DC") = general.nz(Value, "") : guardarDV()
                tabla.AcceptChanges()
            Else
                dvForm(PA).Row("DC") = general.nz(Value, "") : guardarDV()
            End If
        End Set
    End Property

    Private mNOMBANC As String
    Public Property NOMBANC() As String
        Get
            Try
                mNOMBANC = general.nz(dvForm(PA).Row("NOMBANC"), "")
            Catch ex As Exception : End Try
            Return general.nz(mNOMBANC, "")
        End Get
        Set(ByVal Value As String)
            mNOMBANC = general.nz(Value, "")
            If tabla.GetChanges Is Nothing Then
                dvForm(PA).Row("NOMBANC") = general.nz(Value, "") : guardarDV()
                tabla.AcceptChanges()
            Else
                dvForm(PA).Row("NOMBANC") = general.nz(Value, "") : guardarDV()
            End If
        End Set
    End Property

    Private mNUMEROPAG As Integer
    Public Property NUMEROPAG() As Integer
        Get
            Try
                mNUMEROPAG = nzn(dvForm(PA).Row("NUMEROPAG"), 0)
            Catch ex As Exception : End Try
            Return nzn(mNUMEROPAG, 0)
        End Get
        Set(ByVal Value As Integer)
            mNUMEROPAG = nzn(Value, 0)
            If tabla.GetChanges Is Nothing Then
                dvForm(PA).Row("NUMEROPAG") = nzn(Value, 0) : guardarDV() : Debug.WriteLine(Value)
                tabla.AcceptChanges()
            Else
                dvForm(PA).Row("NUMEROPAG") = nzn(Value, 0) : guardarDV() : Debug.WriteLine(Value)
            End If
        End Set
    End Property
    Private mDIESPAG As Integer
    Public Property DIESPAG() As Integer
        Get
            Try
                mDIESPAG = nzn(dvForm(PA).Row("DIESPAG"), 0)
            Catch ex As Exception : End Try
            Return nzn(mDIESPAG, 0)
        End Get
        Set(ByVal Value As Integer)
            mDIESPAG = nzn(Value, 0)
            If tabla.GetChanges Is Nothing Then
                dvForm(PA).Row("DIESPAG") = nzn(Value, 0) : guardarDV() : Debug.WriteLine(Value)
                tabla.AcceptChanges()
            Else
                dvForm(PA).Row("DIESPAG") = nzn(Value, 0) : guardarDV() : Debug.WriteLine(Value)
            End If
        End Set
    End Property

    Private mVEZ1FORPAG As Integer
    Public Property VEZ1FORPAG() As Integer
        Get
            Try
                mVEZ1FORPAG = nzn(dvForm(PA).Row("VEZ1FORPAG"), 0)
            Catch ex As Exception : End Try
            Return nzn(mVEZ1FORPAG, 0)
        End Get
        Set(ByVal Value As Integer)
            mVEZ1FORPAG = nzn(Value, 0)
            If tabla.GetChanges Is Nothing Then
                dvForm(PA).Row("VEZ1FORPAG") = nzn(Value, 0) : guardarDV() : Debug.WriteLine(Value)
                tabla.AcceptChanges()
            Else
                dvForm(PA).Row("VEZ1FORPAG") = nzn(Value, 0) : guardarDV() : Debug.WriteLine(Value)
            End If
        End Set
    End Property

    Private mNOMFORMA As String
    Public Property NOMFORMA() As String
        Get
            Try
                mNOMFORMA = general.nz(dvForm(PA).Row("NOMFORMA"), "")
            Catch ex As Exception : End Try
            Return general.nz(mNOMFORMA, "")
        End Get
        Set(ByVal Value As String)
            mNOMFORMA = general.nz(Value, "")
            If tabla.GetChanges Is Nothing Then
                dvForm(PA).Row("NOMFORMA") = general.nz(Value, "") : guardarDV()
                tabla.AcceptChanges()
            Else
                dvForm(PA).Row("NOMFORMA") = general.nz(Value, "") : guardarDV()
            End If
        End Set
    End Property

#End Region

#Region "INICIALIZAR"

    Public Sub New(ByVal tabla As DataTable, ByVal centro As String, ByRef bindingcontext As BindingContext, ByVal doc As Char, ByVal tip As Char, ByVal SOLOCLASE As Boolean, ByVal nuevoRegistro As Boolean, ByVal proveedor As Integer)

        MyBase.New(tabla, centro, bindingcontext)
        Dim sqlSel As String
        Try
            documento = doc
            tipo = tip
            Me.soloClase = SOLOCLASE

            Select Case documento
                Case OrdenFabComplementos
                    sqlSinWhere = " SELECT cactur.*, " & _
                                        " TALLERS.NOM AS NOMPROVE FROM " & tabla.TableName & " " & _
                                        " LEFT JOIN tallers ON (TALLERS.CODI = cactur.PROVE) "
                Case Albaran
                    Select Case tipo
                        Case Hilos
                            sqlSinWhere = "SELECT cactur.*, prove.NOM AS NOMPROVE, tallers.NOM AS NOMTALLERENTREGA " & _
                                            " FROM  " & tabla.TableName & " " & _
                                            " LEFT JOIN TALLERS ON (TALLERS.CODI = CACTUR.TALLER) " & _
                                            " LEFT JOIN prove ON (prove.CODI = cactur.PROVE) "
                    End Select
                    sqlSinWhere = "SELECT cactur.*, prove.NOM AS NOMPROVE " & _
                                        " FROM  " & tabla.TableName & " " & _
                                        " LEFT JOIN prove ON (prove.CODI = cactur.PROVE) "
                Case Else
                    sqlSinWhere = "SELECT cactur.*, prove.NOM AS NOMPROVE " & _
                                        " FROM  " & tabla.TableName & " " & _
                                        " LEFT JOIN prove ON (prove.CODI = cactur.PROVE) "

            End Select
            If documento = OrdenFabComplementos Then

            Else
            End If
            sqlSel = sqlSinWhere & genWhere() & " LIMIT 1"

            cmdSel.CommandText = sqlSel
            da.SelectCommand = cmdSel

            numeroRegistros = obtenerNumeroReg(tabla.TableName, "")
            Try
                UltimoCodigo = CargaParametro("" & documento & tipo & "C" & centro & "")
                numeroRegistroActual = ObtenerNumeroRegistro(UltimoCodigo)
                If numeroRegistroActual = -1 Then
                    CargarRegistro(ultimo)
                Else
                    CambiarAReg(UltimoCodigo, "", iraregistro)
                End If

            Catch ex As Exception
                CargarRegistro(ultimo)
            End Try
            'da.Fill(tabla)
            dvForm.Sort = "FRA"



            CargarTablas()
            CargarIdentificadores()
            AñadirColumnasPropias()

            If nuevoRegistro Then
                Dim dr As DataRow
                dr = tabla.NewRow
                'CopiarFila(tabla.Rows(0), dr)
                tabla.Clear()
                dr.Item("PROVE") = proveedor '1000
                dr.Item("CENTRO") = general.nz(centro, empresaPorDefecto)
                dr.Item("DOCUMENT") = documento
                dr.Item("FRA") = GetNumeroUltimoPAFCompra(documento, tipo, centro) + 1

                dr.Item("DATA") = Date.Now
                dr.Item("TRASPAS") = False
                dr.Item("TIPUS") = tipo
                tabla.Rows.Add(dr)
                ActualizarNum("CACTUR", DOCUMENT, centro, FRA, TIPUS)
                ActualizarOrigen(True)
            End If
            IniciarLineas()
            lineasCompra.IniciarDesplegableArticulo()
            MoverActual()
            tabla.AcceptChanges()
            DespertarHandlers()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Public Sub New(ByVal tabla As DataTable, ByVal centro As String, ByRef bindingcontext As BindingContext, ByVal doc As Char, ByVal tip As Char)

        MyBase.New(tabla, centro, bindingcontext)
        Dim sqlSel As String
        Try
            documento = doc
            tipo = tip
            Select Case documento
                Case OrdenFabComplementos
                    sqlSinWhere = " SELECT cactur.*, " & _
                                        " TALLERS.NOM AS NOMPROVE FROM " & tabla.TableName & " " & _
                                        " LEFT JOIN tallers ON (TALLERS.CODI = cactur.PROVE) "
                Case Albaran
                    Select Case tipo
                        Case Hilos
                            sqlSinWhere = "SELECT cactur.*, prove.NOM AS NOMPROVE, tallers.NOM AS NOMTALLERENTREGA " & _
                                            " FROM  " & tabla.TableName & " " & _
                                            " LEFT JOIN TALLERS ON (TALLERS.CODI = CACTUR.TALLER) " & _
                                            " LEFT JOIN prove ON (prove.CODI = cactur.PROVE) "
                    End Select
                    sqlSinWhere = "SELECT cactur.*, prove.NOM AS NOMPROVE " & _
                                        " FROM  " & tabla.TableName & " " & _
                                        " LEFT JOIN prove ON (prove.CODI = cactur.PROVE) "
                Case Else
                    sqlSinWhere = "SELECT cactur.*, prove.NOM AS NOMPROVE " & _
                                        " FROM  " & tabla.TableName & " " & _
                                        " LEFT JOIN prove ON (prove.CODI = cactur.PROVE) "

            End Select
            If documento = OrdenFabComplementos Then

            Else
            End If
            sqlSel = sqlSinWhere & genWhere() & " LIMIT 1"

            cmdSel.CommandText = sqlSel
            da.SelectCommand = cmdSel

            numeroRegistros = obtenerNumeroReg(tabla.TableName, "")
            Try
                UltimoCodigo = CargaParametro("" & documento & tipo & "C" & centro & "")
                numeroRegistroActual = ObtenerNumeroRegistro(UltimoCodigo)
                If numeroRegistroActual = -1 Then
                    CargarRegistro(ultimo)
                Else
                    CambiarAReg(UltimoCodigo, "", iraregistro)
                End If

            Catch ex As Exception
                CargarRegistro(ultimo)
            End Try
            'da.Fill(tabla)
            dvForm.Sort = "FRA"

            IniciarLineas()


            CargarTablas()
            CargarIdentificadores()
            AñadirColumnasPropias()
            lineasCompra.IniciarDesplegableArticulo()
            MoverActual()

            tabla.AcceptChanges()
            DespertarHandlers()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Public Overrides Sub CargarIdentificadores()
        Dim dt As New DataTable
        Dim i As Integer
        Try
            Dim c As New MySqlCommand("SELECT FRA, PROVE, CENTRO FROM CACTUR " & _
                                        " WHERE TIPUS = """ & tipo & """ AND " & _
                                        " DOCUMENT = """ & documento & """", cnn)

            If Not IDProve = -1 Then
                c.CommandText = c.CommandText & " AND PROVE = """ & IDProve & """"
            End If
            ACN()
            'dt.SelectCommand = c
            Dim da As New MySqlDataAdapter(c)
            da.Fill(dt)
            'dt.Fill()

            dtIdentificadores = dt
            'Le ponemos los nombres a los proveedores
            dtIdentificadores.Columns("FRA").Caption = rm.GetString("NUMERO")
            dtIdentificadores.Columns("PROVE").Caption = rm.GetString("PROVEEDOR")
            If Not dtIdentificadores.Columns.Contains("NOMPROVE") Then dtIdentificadores.Columns.Add(New DataColumn("NOMPROVE"))
            dtIdentificadores.Columns("NOMPROVE").Caption = rm.GetString("NOMPROVE")
            For i = 0 To dtIdentificadores.Rows.Count - 1
                dtIdentificadores.Rows(i).Item("NOMPROVE") = general.OBN(dtIdentificadores.Rows(i).Item("PROVE"), dtProve)
            Next
            dvIdentificadores = dtIdentificadores.DefaultView

            If mProveSel = -1 Then : dvIdentificadores.RowFilter = "CENTRO = '" & centro & "'"
            Else : dvIdentificadores.RowFilter = "PROVE = '" & PROVE & "' AND CENTRO = '" & centro & "'" : End If

            dvIdentificadores.Sort = "FRA"

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Sub
    Private Sub AñadirColumnasPropias()
        Try
            tabla.Columns.Add(New DataColumn("DIA1"))
            tabla.Columns("DIA1").Caption = rm.GetString("DIA1")
            tabla.Columns.Add(New DataColumn("DIA2"))
            tabla.Columns("DIA2").Caption = rm.GetString("DIA2")
            tabla.Columns.Add(New DataColumn("DIA3"))
            tabla.Columns("DIA3").Caption = rm.GetString("DIA3")
            tabla.Columns.Add(New DataColumn("FORMA"))
            tabla.Columns("FORMA").Caption = rm.GetString("FORMAPAG")
            tabla.Columns.Add(New DataColumn("cta"))
            tabla.Columns("cta").Caption = rm.GetString("CUENTA")

            tabla.Columns.Add(New DataColumn("DTOFORMA"))
            tabla.Columns("DTOFORMA").Caption = rm.GetString("DTOFORMA")
            tabla.Columns.Add(New DataColumn("BANC"))
            tabla.Columns("BANC").Caption = rm.GetString("BANCO")
            tabla.Columns.Add(New DataColumn("NOMBANC"))
            tabla.Columns.Add(New DataColumn("DC"))
            tabla.Columns.Add(New DataColumn("OFI"))
            tabla.Columns.Add(New DataColumn("COFI"))
            tabla.Columns.Add(New DataColumn("NUMEROPAG"))
            tabla.Columns.Add(New DataColumn("DIESPAG"))
            tabla.Columns.Add(New DataColumn("VEZ1FORPAG"))
            tabla.Columns.Add(New DataColumn("NOMFORMA"))

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False
        End Try
    End Sub

#End Region

#Region "CARGA TABLAS"

    Private Sub CargarTablas()
        CargatablaProveedores()
        CargaTablaForma()
        CargaTabla(tablaBancos, CCBancs, CNBancs, dtBancs, True)
        If DOCUMENT = Albaran AndAlso TIPUS = Hilos Then CargaTabla(tablaTalleres, CCTallers, CNTallers, dtTallers, True) : CargaTablasOrdenesFab()
        CargarClientesConPAF()
    End Sub
    Private Sub CargaTablasOrdenesFab()
        Try
            Dim cmdSelect As New MySqlCommand
            Dim daSelect As New MySqlDataAdapter

            cmdSelect.Connection = cnn
            cmdSelect.CommandText = " SELECT CACTUR.FRA FROM " & _
                        " CACTUR WHERE " & _
                        " CENTRO = """ & centro & """ AND " & _
                        " DOCUMENT = """ & OrdenFabComplementos & """ AND " & _
                        " TIPUS = """ & Muestra & """ "


            daSelect.SelectCommand = cmdSelect
            ACN()
            dtOrdeneFabComplementos.Rows.Clear()
            daSelect.Fill(dtOrdeneFabComplementos)
            dtOrdeneFabComplementos.Columns("FRA").Caption = rm.GetString("NUMERO")
            dtOrdeneFabComplementos.DefaultView.Sort = "FRA"

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub CargarClientesConPAF()
        Dim cmd As New MySqlCommand("SELECT DISTINCT PROVE AS CODI FROM CACTUR WHERE TIPUS = """ & tipo & """ AND DOCUMENT = """ & documento & """ AND CENTRO = """ & centro & """ ", cnn)
        Dim dar As MySqlDataReader
        Dim dr As DataRow
        Dim dc1 As New DataColumn("CODI")
        Dim dc2 As New DataColumn("NOM")
        Dim i As Integer
        Try
            ACN()
            dar = cmd.ExecuteReader
            dtProveConPAF = New DataTable
            dtProveConPAF.Columns.Clear()
            dtProveConPAF.Columns.Add(dc1)
            dtProveConPAF.Columns("CODI").Caption = rm.GetString("CODIGO")
            dtProveConPAF.Columns.Add(dc2)
            dtProveConPAF.Columns("NOM").Caption = rm.GetString("NOMBRE")

            While dar.Read
                dr = dtProveConPAF.NewRow
                dr("CODI") = dar("CODI")
                dtProveConPAF.Rows.Add(dr)
            End While

            'Le ponemos los nombres a los proveedores
            For i = 0 To dtProveConPAF.Rows.Count - 1
                dtProveConPAF.Rows(i).Item("NOM") = general.OBN(dtProveConPAF.Rows(i).Item("CODI"), dtProve)
            Next
            dtProveConPAF.DefaultView.Sort = "NOM"
            dar.Close()
            CCN()

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Sub
    Private Sub CargaTablaForma()
        Try
            Dim cmdSelect As New MySqlCommand
            Dim daSelect As New MySqlDataAdapter

            cmdSelect.Connection = cnn
            cmdSelect.CommandText = " SELECT FORPAG.CODI, forpag.nro AS NUMEROPAG, " & _
            " forpag.dies AS DIESPAG, forpag.V_1 AS VEZ1FORPAG, forpag.DESCRIPCIO AS NOMFORMA FROM FORPAG "


            daSelect.SelectCommand = cmdSelect
            ACN()
            dtForpag.Rows.Clear()
            daSelect.Fill(dtForpag)
            dtForpag.DefaultView.Sort = "NOMFORMA"

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub CargatablaProveedores()
        Try
            Dim cmdSelect As New MySqlCommand
            Dim daSelect As New MySqlDataAdapter

            cmdSelect.Connection = cnn
            cmdSelect.CommandText = _
                        " SELECT prove.CODI, prove.NOM, " & _
                        " IVA.IVA, IVA.RE, " & _
                        " prove.DIA1,prove.DIA2,prove.DIA3, " & _
                        " prove.FORMA,prove.CTA,  " & _
                        " prove.BANC, prove.COFI, prove.OFI,    " & _
                        " prove.DC FROM PROVE LEFT JOIN IVA ON (IVA.CODI = PROVE.IVA) "

            daSelect.SelectCommand = cmdSelect
            ACN()
            dtProve.Rows.Clear()
            daSelect.Fill(dtProve)
            dtProve.DefaultView.Sort = "CODI"

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#End Region

#Region "DESPLAZAMIENTO"

    Public Overloads Sub SiguienteReg()
        Try
            ACN()
            ActualizarOrigen()
            MyBase.SiguienteReg()
            MoverActual()
            CCN()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Public Overloads Sub AnteriorReg()
        Try
            ACN()
            ActualizarOrigen()
            Me.CargarRegistro(anterior)
            MoverActual()
            CCN()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Public Overloads Sub UltimoReg()
        Try
            ACN()
            ActualizarOrigen()
            Me.CargarRegistro(ultimo)
            MoverActual()
            CCN()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Public Overloads Sub PrimeroReg()
        Try
            ACN()
            ActualizarOrigen()
            Me.CargarRegistro(primero)
            MoverActual()
            CCN()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Public Overloads Sub CambiarAReg(ByVal id As String, ByVal accion As Integer)
        Dim sql As String
        Try
            ActualizarOrigen()
            sql = "SELECT CODI FROM TALLERS WHERE (CENTRO = """ & centro & """ AND NOM = """ & id & """)"
            MyBase.CambiarAReg(id, "", accion)
            MoverActual()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#End Region

#Region "ORGANIZAR"

    Private Sub IniciarLineas()
        Try
            Dim dt As New aura2k3.ds11.dcactuDataTable
            lineasCompra = New clsLineasCompra(dt, Me.centro, Me.bc, Me)

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Public Sub AñadirRegistro(ByVal prove As Integer)
        Dim drNew As DataRow
        Dim cm As CurrencyManager
        Try
            tabla.Clear()
            cm = bc(dvForm)
            drNew = tabla.NewRow()

            drNew.Item("CENTRO") = general.nz(centro, empresaPorDefecto)
            drNew.Item("DOCUMENT") = documento
            drNew.Item("FRA") = GetNumeroUltimoPAFCompra(documento, tipo, centro) + 1
            drNew.Item(tablaProveedores) = prove
            drNew.Item("DATA") = Date.Now
            drNew.Item("TRASPAS") = False
            drNew.Item("TIPUS") = tipo

            tabla.Rows.Add(drNew)
            cm.Position = 1
            Try
                guardarDV()
            Catch ex As Exception
            End Try
            numeroRegistroActual = numeroRegistroActual + 1
            MoverActual()
            ActualizarOrigen()
            actualizarNumeraciones()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Public Overrides Sub NuevoRegistro()
        Dim drNew As DataRow
        Dim cm As CurrencyManager
        Try
            ActualizarOrigen()
            tabla.Clear()
            cm = bc(dvForm)
            drNew = tabla.NewRow()

            drNew.Item("CENTRO") = general.nz(centro, empresaPorDefecto)
            drNew.Item("DOCUMENT") = documento
            drNew.Item("FRA") = GetNumeroUltimoPAFCompra(documento, tipo, centro) + 1

            drNew.Item("DATA") = Date.Now
            drNew.Item("TRASPAS") = False
            drNew.Item("TIPUS") = tipo

            tabla.Rows.Add(drNew)
            cm.Position = 1
            Try
                guardarDV()
            Catch ex As Exception
            End Try
            numeroRegistroActual = numeroRegistroActual + 1
            MoverActual()
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Private Sub CanviarLineas()

        Try
            If Not PA() = -1 Then lineasCompra.CambioDetalle(centro, Me)

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Public Sub MoverActual()
        Try
            CanviarLineas()
            If TIPUS = Muestra Then lineasCompra.IniciarDesplegableArticulo()
            PonerNombres()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try

    End Sub
    Friend Overrides Sub PonerNombres()
        Dim aceptarCambiosAlFinal As Boolean = False
        Try
            If tabla.GetChanges Is Nothing Then aceptarCambiosAlFinal = True
            'NOMREPRES = general.OBN(REPRES, dtRepres)

            NOMPROVE = general.OBN(PROVE, dtProve)
            If DOCUMENT = Albaran AndAlso TIPUS = Hilos Then NOMTALLER = general.OBN(TALLER, dtTallers)

            DIA1 = general.OBN(PROVE, dtProve, "DIA1", "CODI", True)
            DIA2 = general.OBN(PROVE, dtProve, "DIA2", "CODI", True)
            DIA3 = general.OBN(PROVE, dtProve, "DIA3", "CODI", True)
            FORMA = general.OBN(PROVE, dtProve, "FORMA")
            cta = general.OBN(PROVE, dtProve, "CTA")
            COFI = general.OBN(PROVE, dtProve, "COFI")
            OFI = general.OBN(PROVE, dtProve, "OFI")

            BANC = general.OBN(BANC, dtBancs, "CODI")
            NOMBANC = general.OBN(BANC, dtBancs, "DESCRIPCIO")
            NUMEROPAG = general.OBN(FORMA, dtForpag, "NUMEROPAG", "CODI", True)
            DIESPAG = general.OBN(FORMA, dtForpag, "DIESPAG", "CODI", True)
            VEZ1FORPAG = general.OBN(FORMA, dtForpag, "VEZ1FORPAG", "CODI", True)
            NOMFORMA = general.OBN(FORMA, dtForpag, "NOMFORMA")


            Select Case CDate(general.nz(DATA, 0))
                Case Is >= CDate("#1/7/2010#")
                    P_IVA1 = general.OBN(PROVE, dtProve, "IVA", "CODI", True)
                Case Else
                    If P_IVA1 <> general.OBN(PROVE, dtProve, "IVA", "CODI", True) Then
                        If general.nz(P_IVA1, 0) = 0 Then P_IVA1 = general.OBN(PROVE, dtProve, "IVA", "CODI", True)
                    Else
                        'NO TOCO NADA DEJO EL IVA POR DEFECTO
                    End If
            End Select


            If P_RE1 <> general.OBN(PROVE, dtProve, "RE", "CODI", True) Then
                P_RE1 = general.OBN(PROVE, dtProve, "RE", "CODI", True)
            End If

            If aceptarCambiosAlFinal AndAlso (Not tabla.GetChanges Is Nothing) Then tabla.AcceptChanges()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False
        End Try
    End Sub
    Private Function ComprobarActualizacion() As Double
        Dim numer As Double
        Try
            numer = GetNumeroUltimoPAFCompra(DOCUMENT, TIPUS, centro) + 1
            If numer <> FRA Then
                Return numer
            End If
            Return FRA

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Function
    Public Sub actualizarNumeraciones()
        Try
            FRA = ComprobarActualizacion()
            ActualizarNum("CACTUR", DOCUMENT, centro, FRA, TIPUS)

        Catch ex As Exception
            LOG(ex.ToString)
        End Try
    End Sub
    Public Overrides Sub ActualizarOrigen(Optional ByVal nocerrar As Boolean = False, Optional ByVal hackDetalle As Boolean = False)
        Try
            MyBase.ActualizarOrigen(True)
            if lineasCompra IsNot Nothing Then lineasCompra.ActualizarOrigen(True, True)

        Catch ex As Exception
            If ex.ToString.Substring(0, 9) = "Duplicate" Then
                'La única posibilidad de que este dupliacado es que alguien haya insertado otro antes
                FRA = GetNumeroUltimoPAFCompra(DOCUMENT, TIPUS, centro) + 1
                Try
                    MyBase.ActualizarOrigen(True)
                    lineasCompra.ActualizarOrigen(True, True)
                Catch ex2 As Exception
                    'Throw ex
                End Try
            End If
            'Throw ex
            'LOG(ex.ToString) 
            cargando = False : CCN()
        End Try
    End Sub
    Private Function ObtenerDocNuevo(ByVal afacturaproforma As Boolean) As Object
        Try
            If afacturaproforma = True Then
                Return "M"
            End If
            Select Case documento
                Case "C" : Return "A"
                Case "A" : Return "F"
            End Select

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Function
    Public Overloads Sub borrar()
        Try
            ACN()
            lineasCompra.BorrarActualDVDetalle(True)
            MyBase.borrar()
            MoverActual()
            CCN()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub
    Public Sub PonerDatosProve()
        Try
            PonerNombres()
            guardarDV()
            'lineasCompra.dvMuestras.RowFilter = "PROVE = '" & PROVE & "'"

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#End Region

#Region "CALCULOS"

    Friend Sub ActualizarTotalesPAF()
        Try
            If Not DOCUMENT = OrdenFabComplementos Then
                If Not PA() = -1 Then
                    If Not tabla.GetChanges Is Nothing Or Not lineasCompra.tabla.GetChanges Is Nothing Then
                        'If BRUT1 <> sumaTotal("IMPORT", lineasCompra.dvForm) Then
                        BRUT1 = roundnum(sumaTotal("IMPORT", lineasCompra.dvForm), 2)
                        DTE1 = roundnum((P_DTE / 100) * BRUT1, 2)
                        BASE1 = roundnum(BRUT1 - DTE1, 2)
                        IVA1 = roundnum(BASE1 * IIf(nzn(P_IVA1, 0) = -1, 0, nzn(P_IVA1, 0)) / 100, 2)
                        RE1 = roundnum(BASE1 * IIf(nzn(P_RE1, 0) = -1, 0, nzn(P_RE1, 0)) / 100, 2)
                        TOTAL = roundnum(BASE1 + IVA1 + RE1, 2)
                        'End If
                    End If
                End If
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
            CCN()
        End Try
    End Sub
    Friend Sub DespertarHandlers()
        AddHandler tabla.ColumnChanged, New DataColumnChangeEventHandler(AddressOf CanviarColumnaPAF)
    End Sub
    Friend Sub DormirHandlers()
        RemoveHandler tabla.ColumnChanged, New DataColumnChangeEventHandler(AddressOf CanviarColumnaPAF)
    End Sub
    Private Sub CanviarColumnaPAF(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)

        Try
            guardarDV()
            If Not tabla.GetChanges Is Nothing Then
                DormirHandlers()
                ActualizarTotalesPAF()
                DespertarHandlers()
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
            CCN()
        End Try

    End Sub

#End Region

#Region "OVERRIDES"

    Friend Overrides Function TieneCambios() As Boolean
        Try
            guardarDV()
            If Not tabla.GetChanges Is Nothing OrElse lineasCompra.TieneCambios Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Function
    Private Function HaySeleccion() As Boolean
        Try
            If mProveSel = -1 Then : Return False
            Else : Return True : End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Function
    Friend Overrides Function genWhere() As String
        Dim ret As String
        Try

            If HaySeleccion() Then
                ret = " WHERE cactur.CENTRO = """ & general.nz(centro, empresaPorDefecto) & """ AND DOCUMENT = """ & _
                        documento & """ AND  TIPUS = """ & tipo & """ AND PROVE = " & mProveSel & " "
            Else
                ret = " WHERE cactur.CENTRO = """ & general.nz(centro, empresaPorDefecto) & """ AND DOCUMENT = """ & _
                        documento & """ AND TIPUS = """ & tipo & """ "
            End If

            Return ret

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Function
    Friend Overrides Function GenOrder() As String
        Try
            Return " ORDER BY FRA "

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Function
    Friend Function genWhereSubSelect(ByVal esCambio As Boolean) As String
        Try
            If esCambio AndAlso Not HaySeleccion() Then
                Return " M1.PROVE = " & mProveSel & " AND TIPUS = """ & tipo & """ AND DOCUMENT = """ & documento & """ AND CENTRO = """ & centro & """ AND "
            Else
                If HaySeleccion() Then
                    Return " PROVE = " & mProveSel & " AND TIPUS = """ & tipo & """ AND DOCUMENT = """ & documento & """ AND CENTRO = """ & centro & """ AND "
                Else
                    Return " DOCUMENT = """ & documento & """ AND TIPUS = """ & tipo & """ AND CENTRO = """ & centro & """ AND "
                End If
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Function
    Private Function GenWhereConTodo(ByVal id As Object, ByVal esCambio As Boolean) As String
        Dim ret As String
        Try
            If Not HaySeleccion() Then
                'Debe ser un cambio Como id seguro que no será nothing mirmaos cual es el numero
                'Aqui nunca se llega
                ret = " WHERE FRA = """ & id & """ AND TIPUS = """ & tipo & """ AND DOCUMENT = """ & documento & """ AND " & WCNoTabla() & " "
            Else
                'Que sea nothing quiere decir que ha habido un ir a registro
                If id Is Nothing OrElse esCambio Then
                    'DOCUMENT = """ & DOCUMENT & """ AND 
                    ret = " WHERE  " & WCNoTabla() & " AND PROVE = """ & mProveSel & """ AND TIPUS = """ & tipo & """ AND DOCUMENT = """ & documento & """ "
                Else
                    ret = " WHERE  " & WCNoTabla() & " AND TIPUS = """ & tipo & """ AND DOCUMENT = """ & documento & """ " & _
                            " AND FRA = """ & id & """ AND PROVE = """ & mProveSel & """ "
                End If
            End If

            Return ret
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Function
    Friend Overrides Function ObtenerNumeroRegistro(ByVal id As Object) As Integer
        Dim esCambio As Boolean = False
        Dim idx As Object
        If id Is Nothing Then : id = FRA : Else
            If id.GetType Is GetType(Integer) Then
                If id = esCambioSeleccion Then : esCambio = True : End If
            End If
        End If
        Dim cmdSelect As MySqlCommand
        Try

            cmdSelect = New MySqlCommand
            cmdSelect.CommandText = " SELECT (SELECT COUNT(*) " & _
                            " FROM " & t() & " AS M2 WHERE " & genWhereSubSelect(esCambio) & " " & _
                            " M2.FRA < M1.FRA) AS rownum FROM " & t() & " AS M1 " & _
                            " " & GenWhereConTodo(id, esCambio) & " " & GenOrder()

            cmdSelect.Connection = cnn
            idx = cmdSelect.ExecuteScalar
            If idx Is Nothing Then Return -1
            Return idx

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Function
    Friend Overrides Function genWhereNumeroRegistros() As String
        Dim ret As String
        Try
            If Not HaySeleccion() Then
                ret = " WHERE cactur.CENTRO = """ & general.nz(centro, empresaPorDefecto) & """ AND DOCUMENT = """ & documento & """ AND TIPUS = """ & tipo & """ "
            Else
                ret = " WHERE cactur.CENTRO = """ & general.nz(centro, empresaPorDefecto) & """ AND DOCUMENT = """ & documento & """ " & _
                            " AND TIPUS = """ & tipo & """ AND PROVE = " & mProveSel & " "
            End If

            Return ret
        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Function
    Public Overloads Function cambioCentro(ByVal centro As Char, ByVal iralregistro As Integer) As Boolean
        Try
            MyBase.cambioCentro(centro, iralregistro)
            MoverActual()
            lineasCompra.IniciarDesplegableArticulo()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try

    End Function

#End Region

#Region "IMPRIMIR"

#Region "OBTENER STRINGS"

    Private Function ObtenerFechaDePedido(ByVal fra1 As Integer) As DateTime
        Dim cmdSel As MySqlCommand
        Dim d As DateTime
        Try
            cmdSel = New MySqlCommand("SELECT DATA FROM FACTUR WHERE DOCUMENT = ""C"" AND FRA = " & fra1 & " AND CENTRO = """ & centro & """", cnn)
            ACN()
            d = cmdSel.ExecuteScalar
            CCN()
            Return d.ToShortDateString

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Function
    Private Function ObtenerPedidoDeFactura() As Integer

        Try
            'El problema es que una facuta puede venir de varias pedidos
            Return lineasCompra.COMAN

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Function
    Private Function obtenerDatosProve(ByVal prove As Integer, ByVal xls As ExcelDirecto.ExcelDirecto) As String
        Dim cmdSel As New MySqlCommand
        Dim str As String
        Dim dareader As MySqlDataReader
        Try

            str = "SELECT NOM, DOM, CP, PROV, POB, NIF, EMAIL1 " & _
                " FROM " & tablaProveedores & " WHERE CODI = """ & prove & """ "
            cmdSel.CommandText = str
            cmdSel.Connection = cnn
            ACN()
            dareader = cmdSel.ExecuteReader

            While dareader.Read
                With dareader
                    str = .Item("NOM") & vbCrLf & .Item("DOM") & vbCrLf & _
                        .Item("CP") & " " & .Item("POB") & vbCrLf & .Item("PROV") & vbCrLf & .Item("NIF")
                End With
            End While
            dareader.Close()
            CCN()
            Return str

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Function

#End Region

    Public Sub ImprimirPAF()
        Dim clsImp As clsImpresionPAFPO
        Try
            clsImp = New clsImpresionPAFPO(DOCUMENT, TIPUS, Compra, Me.dvForm, lineasCompra.dvForm, False)
            clsImp.ImprimirDocumento(False)

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#End Region

#Region "ELECCION"

    Public Sub ElegirProve(ByVal prove As Integer)
        Try
            ACN()
            mProveSel = prove
            numeroRegistros = obtenerNumeroReg(t, "")
            If prove <> -1 Then
                If numeroRegistros = 0 Then Exit Sub
                Me.CargarRegistro(ultimo, esCambioSeleccion)
                MoverActual()
            Else
                numeroRegistroActual = ObtenerNumeroRegistro(Nothing)
            End If

            If mProveSel = -1 Then : dvIdentificadores.RowFilter = "CENTRO = '" & centro & "'"
            Else : dvIdentificadores.RowFilter = "PROVE = '" & prove & "' AND CENTRO = '" & centro & "'" : End If
            CCN()

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#End Region

#Region "TRASPASAR"

    Public Sub TraspasarLinea()
        Dim f As New frmListaRecibido
        Dim dv As New DataView
        Try
            If TRASPAS = False Then
                f.PAF = Me
                f.ShowDialog()
                f = Nothing
            Else
                MessageBox.Show(rm.GetString("NOTRASPLINIASDOCTRAS"))
            End If

        Catch ex As Exception
            LOG(ex.ToString) : cargando = False : CCN()
        End Try
    End Sub

#End Region

End Class