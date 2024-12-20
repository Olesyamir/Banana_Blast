<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:jv="http://www.univ-grenoble-alpes.fr/l3miage/jeu_video"
                xmlns:par="http://www.univ-grenoble-alpes.fr/partie">

    <!-- Sortie en HTML -->
    <xsl:output method="html" indent="yes" encoding="UTF-8"/>

    <!-- Paramètre du nom du joueur à rechercher -->
    <xsl:param name="monnom"/>

    <!-- Template racine -->
    <xsl:template match="/">
        <html>
            <head>
                <title>Profil du Joueur</title>
                <link rel="stylesheet" href="../css/top10Stylesheet.css"/>
            </head>
            <body>
                <h1>Profil de <xsl:value-of select="$monnom"/></h1>

                <!-- Section des parties du joueur -->
                <xsl:apply-templates select="jv:savedGames/jv:parties/par:partie"/>
            </body>
        </html>
    </xsl:template>

    <!-- Template pour chaque partie du joueur -->
    <xsl:template match="par:partie">
        <xsl:if test="par:nomjoueur=$monnom">
        <div>
            <h2>Partie jouée le <xsl:value-of select="par:date"/></h2>
            <p>Score: <xsl:value-of select="par:score"/></p>
        </div>
        </xsl:if>
    </xsl:template>

</xsl:stylesheet>
