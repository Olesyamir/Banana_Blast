<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:fc="http://www.univ-grenoble-alpes.fr/l3miage/files"
                xmlns:par="http://www.univ-grenoble-alpes.fr/partie">

    <!-- Sortie en XML -->
    <xsl:output method="xml" indent="yes" encoding="UTF-8"/>

    <!-- Template racine -->
    <xsl:template match="/" xmlns="http://www.univ-grenoble-alpes.fr/l3miage/jeu_video">
        <savedGames>
            <!-- Appliquer les templates sur les fichiers -->
            <parties>
            <xsl:apply-templates select="fc:fichiers/fc:fichier"/>
            </parties>
        </savedGames>
    </xsl:template>

    <!-- Template pour chaque fichier -->
    <xsl:template match="fc:fichier">
        <!-- Charger le fichier spécifié -->
        <xsl:variable name="nom_fichier" select="concat('../xml/', .)"/>
        <xsl:variable name="fichierXmlCurrent" select="document($nom_fichier)"/>

        <!-- Appliquer les templates sur les parties dans le fichier -->
        
        <xsl:apply-templates select="$fichierXmlCurrent/par:partie"/>
       
    </xsl:template>

    <!-- Template pour chaque partie -->
    <xsl:template match="par:partie" xmlns="http://www.univ-grenoble-alpes.fr/partie">
        <partie>
            <nomjoueur><xsl:value-of select="par:nomjoueur"/></nomjoueur>
            <date><xsl:value-of select="par:date"/></date>
            <score><xsl:value-of select="par:score"/></score>
        </partie>
    </xsl:template>

</xsl:stylesheet>
