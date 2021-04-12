Imports System.Xml

Public NotInheritable Class xmlExtension
    Inherits XmlDocument

    Public Function AddNode(ByVal oSource As XmlNode, ByVal sName As String) As Boolean
        Return AddNode(oSource, sName, Nothing)
    End Function

    Public Function AddNode(ByVal oSource As XmlNode, ByVal sName As String, ByVal sParent As String) As Boolean
        Try
            If Not (sName Is Nothing) AndAlso Not (oSource Is Nothing) Then
                Dim oNewNode As XmlNode = CreateElement(sName)
                oNewNode.InnerXml = oSource.InnerXml
                If Not (sParent Is Nothing) Then
                    sParent = sParent.Trim
                End If
                If sParent Is Nothing OrElse sParent.Equals(String.Empty) Then
                    DocumentElement.AppendChild(oNewNode)
                    Return True
                Else
                    If Not sParent.Substring(0, 2).Equals("//") Then
                        sParent = "//" + sParent
                    End If
                    Dim oParent As XmlNode = SelectSingleNode(sParent)
                    If Not (oParent Is Nothing) Then
                        oParent.AppendChild(oNewNode)
                        Return True
                    End If
                End If
            End If
        Catch generatedExceptionVariable0 As Exception
        End Try
        Return False
    End Function

    Public Function AddNodes(ByVal oSourceList As XmlNodeList, ByVal sName As String) As Boolean
        Return AddNodes(oSourceList, sName, Nothing)
    End Function

    Public Function AddNodes(ByVal oSourceList As XmlNodeList, ByVal sName As String, ByVal sParent As String) As Boolean
        Try
            If Not (oSourceList Is Nothing) Then
                Dim i As Integer = 0
                While i < oSourceList.Count
                    If Not AddNode(oSourceList.Item(i), sName, sParent) Then
                        Return False
                    End If
                    System.Math.Min(System.Threading.Interlocked.Increment(i), i - 1)
                End While
                Return True
            End If
        Catch generatedExceptionVariable0 As Exception
        End Try
        Return False
    End Function

    Public Function MergeNode(ByVal oSource As XmlNode, ByVal sName As String) As Boolean
        Return MergeNode(oSource, sName, Nothing)
    End Function

    Public Function MergeNode(ByVal oSource As XmlNode, ByVal sName As String, ByVal sParent As String) As Boolean
        Try
            If Not (sName Is Nothing) AndAlso Not (oSource Is Nothing) Then
                Dim theNode As XmlNode = Nothing
                If Not (sParent Is Nothing) Then
                    sParent = sParent.Trim
                End If
                If sParent Is Nothing OrElse sParent.Equals(String.Empty) Then
                    theNode = SelectSingleNode("//" + sName)
                    If theNode Is Nothing Then
                        theNode = CreateElement(sName)
                        DocumentElement.AppendChild(theNode)
                    End If
                Else
                    If Not sParent.Substring(0, 2).Equals("//") Then
                        sParent = "//" + sParent
                    End If
                    Dim theParent As XmlNode = SelectSingleNode(sParent)
                    If Not (theParent Is Nothing) Then
                        theNode = theParent.SelectSingleNode(sName)
                        If theNode Is Nothing Then
                            theNode = CreateElement(sName)
                            theParent.AppendChild(theNode)
                        End If
                    End If
                End If
                If Not (theNode Is Nothing) Then
                    theNode.InnerXml += oSource.InnerXml
                    Return True
                End If
            End If
        Catch generatedExceptionVariable0 As Exception
        End Try
        Return False
    End Function

End Class
