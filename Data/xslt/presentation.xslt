<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:savedGames="http://www.univ-grenoble-alpes.fr/l3miage/jeu_video"
                xmlns:profile="http://www.univ-grenoble-alpes.fr/l3miage/profile"
                exclude-result-prefixes="savedGames profile">

    <!-- Déclaration du paramètre pour le nom du joueur -->
    <xsl:param name="destinedName" select="'John Doe'"/>

    <!-- Configuration de la sortie HTML -->
    <xsl:output method="html" encoding="UTF-8" indent="yes"/>

    <!-- Modèle principal -->
    <xsl:template match="/">
        <html>
            <head>
                <title>Profil de <xsl:value-of select="$destinedName"/></title>
                <style>
                    body {
                    font-family: 'Roboto', sans-serif;
                    margin: 0;
                    padding: 0;
                    background: linear-gradient(45deg, #1b1f36, #283048);
                    color: #ffffff;
                    text-align: center;
                    }
                    h1 {
                    font-size: 3em;
                    margin-top: 20px;
                    color: #f8e71c;
                    text-shadow: 2px 2px 10px #f5a623;
                    }
                    h2 {
                    font-size: 2em;
                    margin-top: 30px;
                    color: #50e3c2;
                    text-shadow: 1px 1px 5px #7ed321;
                    }
                    p {
                    font-size: 1.2em;
                    margin: 10px auto;
                    color: #d1d8e0;
                    }
                    table {
                    margin: 20px auto;
                    width: 70%;
                    border-collapse: collapse;
                    box-shadow: 0px 4px 10px rgba(0, 0, 0, 0.3);
                    }
                    th, td {
                    padding: 15px;
                    font-size: 1.1em;
                    border: 1px solid #44475a;
                    text-align: center;
                    }
                    th {
                    background-color: #2e3b4e;
                    color: #f8e71c;
                    text-transform: uppercase;
                    }
                    td {
                    background: rgba(255, 255, 255, 0.1);
                    transition: background 0.3s;
                    }
                    td:hover {
                    background: rgba(255, 255, 255, 0.2);
                    }
                    tr:nth-child(even) td {
                    background: rgba(0, 0, 0, 0.3);
                    }
                    tr:nth-child(even) td:hover {
                    background: rgba(255, 255, 255, 0.2);
                    }
                </style>
            </head>
            <body>
                <!-- Profil du joueur -->
                <h1>Profil de <xsl:value-of select="$destinedName"/></h1>

                <!-- Chargement et vérification du document profile.xml -->
                <xsl:variable name="externalProfileDoc" select="document('../xml/profile.xml')"/>
                <xsl:choose>
                    <!-- Si le joueur existe dans profile.xml -->
                    <xsl:when test="$externalProfileDoc//profile:joueur[profile:nom = $destinedName]">
                        <xsl:variable name="currentPlayer"
                                      select="$externalProfileDoc//profile:joueur[profile:nom = $destinedName]"/>

                        <p>Sexe : <xsl:value-of select="$currentPlayer/profile:sexe"/></p>
                        <p>Âge : <xsl:value-of select="$currentPlayer/profile:age"/></p>
                        <p>Meilleur Score : <xsl:value-of select="$currentPlayer/profile:meilleurscore"/></p>
                    </xsl:when>

                    <!-- Si le joueur n'existe pas -->
                    <xsl:otherwise>
                        <p>Le joueur <xsl:value-of select="$destinedName"/> est introuvable.</p>
                    </xsl:otherwise>
                </xsl:choose>

                <!-- Parties jouées -->
                <h2>Parties Jouées</h2>
                <table>
                    <tr>
                        <th>Date</th>
                        <th>Score</th>
                    </tr>
                    <!-- Filtrer les parties dans sauvegarde_partie.xml -->
                    <xsl:for-each select="savedGames:savedGames/savedGames:parties/savedGames:partie[savedGames:nomjoueur = $destinedName]">
                        <tr>
                            <td><xsl:value-of select="savedGames:date"/></td>
                            <td><xsl:value-of select="savedGames:score"/></td>
                        </tr>
                    </xsl:for-each>
                </table>
            </body>
        </html>
    </xsl:template>

</xsl:stylesheet>
