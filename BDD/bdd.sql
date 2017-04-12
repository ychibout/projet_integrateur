
-- Généré le :  Dim 12 Février 2017 à 00:29
-- Version du serveur :  5.7.14
-- Version de PHP :  5.6.25

-- --------------------------------------------------------

--
-- Structure de la table `amelioration`
--

CREATE TABLE `amelioration` (
  `amelioration_id` int(11) NOT NULL AUTO_INCREMENT PRIMARY KEY,
  `amelioration_nom` varchar(50) NOT NULL,
  `amelioration_pv` int(11) DEFAULT '0',
  `amelioration_vitesse` int(11) DEFAULT '0',
  `amelioration_degat` int(11) DEFAULT '0',
  `amelioration_image` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Structure de la table `arme`
--

CREATE TABLE `arme` (
  `arme_id` int(11) NOT NULL AUTO_INCREMENT PRIMARY KEY,
  `arme_nom` varchar(50) DEFAULT NULL,
  `arme_type` varchar(50) DEFAULT NULL,
  `arme_degat` int(11) DEFAULT '1',
  `arme_munition` int(11) DEFAULT '1',
  `arme_recharge` int(11) DEFAULT '1',
  `arme_image` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Structure de la table `inventaire`
--

CREATE TABLE `inventaire` (
  `inventaire_joueur_id` int(11) NOT NULL,
  `inventaire_materiau_id` int(11) NOT NULL,
  `inventaire_quantite` int(11) DEFAULT '1'
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Structure de la table `joueur`
--

CREATE TABLE `joueur` (
  `joueur_id` int(11) NOT NULL AUTO_INCREMENT PRIMARY KEY,
  `joueur_nom` varchar(50) NOT NULL,
  `joueur_mdp` varchar(40) NOT NULL,
  `joueur_niveau` int(11) DEFAULT '1',
  `joueur_xp` int(11) DEFAULT '0'
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Structure de la table `joueur_vaisseau_amelioration`
--

CREATE TABLE `joueur_vaisseau_amelioration` (
  `jva_joueur_id` int(11) NOT NULL,
  `jva_vaisseau_id` int(11) NOT NULL,
  `jva_amelioration_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Structure de la table `materiau`
--

CREATE TABLE `materiau` (
  `materiau_id` int(11) NOT NULL AUTO_INCREMENT PRIMARY KEY,
  `materiau_nom` varchar(50) NOT NULL,
  `materiau_description` varchar(1024) DEFAULT NULL,
  `materiau_rarete` varchar(50) DEFAULT 'Commun',
  `materiau_image` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Structure de la table `materiau_amelioration`
--

CREATE TABLE `materiau_amelioration` (
  `ma_materiau_id` int(11) NOT NULL,
  `ma_amelioration_id` int(11) NOT NULL,
  `ma_quantite` int(11) DEFAULT '1'
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Structure de la table `vaisseau`
--

CREATE TABLE `vaisseau` (
  `vaisseau_id` int(11) NOT NULL AUTO_INCREMENT PRIMARY KEY,
  `vaisseau_nom` varchar(50) DEFAULT NULL,
  `vaisseau_type` varchar(50) DEFAULT NULL,
  `vaisseau_pv` int(11) DEFAULT NULL,
  `vaisseau_degat` int(11) DEFAULT '0',
  `vaisseau_vitesse` int(11) DEFAULT NULL,
  `vaisseau_image` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Structure de la table `vaisseau_arme`
--

CREATE TABLE `vaisseau_arme` (
  `va_vaisseau_id` int(11) NOT NULL,
  `va_arme_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;


--
-- Index pour la table `inventaire`
--
ALTER TABLE `inventaire`
  ADD PRIMARY KEY (`inventaire_joueur_id`,`inventaire_materiau_id`),
  ADD KEY `inventaire_fk_2` (`inventaire_materiau_id`);


--
-- Index pour la table `joueur_vaisseau_amelioration`
--
ALTER TABLE `joueur_vaisseau_amelioration`
  ADD PRIMARY KEY (`jva_joueur_id`,`jva_amelioration_id`,`jva_vaisseau_id`),
  ADD KEY `jva_fk_2` (`jva_amelioration_id`),
  ADD KEY `jva_fk_3` (`jva_vaisseau_id`);


--
-- Index pour la table `materiau_amelioration`
--
ALTER TABLE `materiau_amelioration`
  ADD PRIMARY KEY (`ma_materiau_id`,`ma_amelioration_id`),
  ADD KEY `am_fk_2` (`ma_amelioration_id`);


--
-- Index pour la table `vaisseau_arme`
--
ALTER TABLE `vaisseau_arme`
  ADD PRIMARY KEY (`va_vaisseau_id`,`va_arme_id`),
  ADD KEY `va_fk_2` (`va_arme_id`);

--
-- Contraintes pour la table `inventaire`
--
ALTER TABLE `inventaire`
  ADD CONSTRAINT `inventaire_fk_1` FOREIGN KEY (`inventaire_joueur_id`) REFERENCES `joueur` (`joueur_id`),
  ADD CONSTRAINT `inventaire_fk_2` FOREIGN KEY (`inventaire_materiau_id`) REFERENCES `materiau` (`materiau_id`);

--
-- Contraintes pour la table `joueur_vaisseau_amelioration`
--
ALTER TABLE `joueur_vaisseau_amelioration`
  ADD CONSTRAINT `jva_fk_1` FOREIGN KEY (`jva_joueur_id`) REFERENCES `joueur` (`joueur_id`),
  ADD CONSTRAINT `jva_fk_2` FOREIGN KEY (`jva_amelioration_id`) REFERENCES `amelioration` (`amelioration_id`),
  ADD CONSTRAINT `jva_fk_3` FOREIGN KEY (`jva_vaisseau_id`) REFERENCES `vaisseau` (`vaisseau_id`);

--
-- Contraintes pour la table `materiau_amelioration`
--
ALTER TABLE `materiau_amelioration`
  ADD CONSTRAINT `am_fk_1` FOREIGN KEY (`ma_materiau_id`) REFERENCES `materiau` (`materiau_id`),
  ADD CONSTRAINT `am_fk_2` FOREIGN KEY (`ma_amelioration_id`) REFERENCES `amelioration` (`amelioration_id`);

--
-- Contraintes pour la table `vaisseau_arme`
--
ALTER TABLE `vaisseau_arme`
  ADD CONSTRAINT `va_fk_1` FOREIGN KEY (`va_vaisseau_id`) REFERENCES `vaisseau` (`vaisseau_id`),
  ADD CONSTRAINT `va_fk_2` FOREIGN KEY (`va_arme_id`) REFERENCES `arme` (`arme_id`);
