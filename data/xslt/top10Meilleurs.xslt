<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:par="http://www.univ-grenoble-alpes.fr/partie"
                xmlns:fc="http://www.univ-grenoble-alpes.fr/l3miage/files"
                xmlns:exsl="http://exslt.org/common">
    
    <!-- Ce fichier sert à générer un HTML présentant la liste des hi-scores -->
    
    <xsl:output method="html"/>
    
    <xsl:template match="/">
        <html lang="fr">
            <head>
                <title> Liste des meilleurs</title>
                <link rel="stylesheet" href="../css/top10Stylesheet.css"/>
            </head>
            <body>
                
                <h1>Hall  Of  Fames</h1>
                
                <!-- Collecte des scores de tous les fichiers -->
                <xsl:variable name="allScores">
                    <!-- Parcourir la liste des fichiers -->
                    <xsl:for-each select="fc:fichiers/fc:fichier">
                        <!-- Charger chaque fichier avec la fonction document() -->
                        <xsl:variable name="nom_fichier" select="concat('../xml/', .)"/>
                        <xsl:variable name="fichierXmlCurrent" select="document($nom_fichier)" />
                        <!-- créer les éléments parties dans $allScores -->
                        <xsl:for-each select="$fichierXmlCurrent/par:partie">
                            <_partie>
                                <_nomjoueur><xsl:value-of select="par:nomjoueur"/></_nomjoueur>
                                <_score><xsl:value-of select="par:score"/></_score>
                            </_partie>
                        </xsl:for-each>
                    </xsl:for-each>
                </xsl:variable>

                <!-- Conversion en node-set car version 1.0 ne permet pas d'utiliser directement $allScores -->
                <xsl:variable name="allScoresNodeSet" select="exsl:node-set($allScores)"/>
                
                <ol>
                     <xsl:for-each select="$allScoresNodeSet/_partie">
                        <xsl:sort select="_score" data-type="number" order="descending"/>
                         <!--- si la position de la noeud partie est < 10 (dans top 10 meilleurs), le prendre -->
                        <xsl:if test="position() &lt;= 10 ">   
                            <li>
                                Joueur: <xsl:value-of select="_nomjoueur"/> | 
                                Score: <xsl:value-of select="_score"/>
                            </li>
                        </xsl:if>
                     </xsl:for-each>
                </ol>
            </body>
            
        </html>
        
    </xsl:template>
    
   
        
    
</xsl:stylesheet>
                