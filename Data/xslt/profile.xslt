<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:jv="http://www.univ-grenoble-alpes.fr/l3miage/jeu_video"
                xmlns:p="http://www.univ-grenoble-alpes.fr/l3miage/profile"
                exclude-result-prefixes="xsl">

    <!-- Template de la transformation -->
    <xsl:template match="/">
        <html>
            <head>
                <title>Profil du Joueur</title>
                <style>
                    body { font-family: Arial, sans-serif; }
                    table { border-collapse: collapse; width: 100%; margin-top: 20px; }
                    th, td { border: 1px solid #ddd; padding: 8px; text-align: left; }
                    th { background-color: #f2f2f2; }
                </style>
            </head>
            <body>
                <h1>Profil du Joueur</h1>

                <!-- Affichage des informations du joueur -->
                <xsl:for-each select="profile/joueurs/joueur">
                    <xsl:if test="nom='Bob'"> <!-- Si le nom du joueur est 'Bob' -->
                        <h2><xsl:value-of select="nom"/> (Age: <xsl:value-of select="age"/>, Sexe: <xsl:value-of select="sexe"/>)</h2>
                        <p>Meilleur Score: <xsl:value-of select="meilleurscore"/></p>
                    </xsl:if>
                </xsl:for-each>

                <!-- Liste des parties de 'Bob' -->
                <h3>Parties Jouées :</h3>
                <table>
                    <tr>
                        <th>Date</th>
                        <th>Score</th>
                    </tr>

                    <xsl:for-each select="savedGames/parties/partie">
                        <xsl:if test="nomjoueur='Bob'">
                            <tr>
                                <td><xsl:value-of select="date"/></td>
                                <td><xsl:value-of select="score"/></td>
                            </tr>
                        </xsl:if>
                    </xsl:for-each>

                </table>

            </body>
        </html>
    </xsl:template>

</xsl:stylesheet>
