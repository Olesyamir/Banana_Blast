<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:games="http://www.univ-grenoble-alpes.fr/l3miage/games"
                exclude-result-prefixes="games">

    <!-- Configuration de la sortie -->
    <xsl:output method="xml" encoding="UTF-8" indent="yes"/>

    <!-- Modèle principal -->
    <xsl:template match="/games:Games">
        <savedGames xmlns="http://www.univ-grenoble-alpes.fr/l3miage/jeu_video"
                    xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
                    xsi:schemaLocation="http://www.univ-grenoble-alpes.fr/l3miage/jeu_video ../xsd/savedGames.xsd">
            <parties>
                <!-- Parcours des parties -->
                <xsl:for-each select="games:parties/games:partie">
                    <partie>
                        <nomjoueur><xsl:value-of select="games:nomjoueur"/></nomjoueur>
                        <date><xsl:value-of select="games:date"/></date>
                        <score><xsl:value-of select="games:score"/></score>
                    </partie>
                </xsl:for-each>
            </parties>
        </savedGames>
    </xsl:template>

</xsl:stylesheet>
