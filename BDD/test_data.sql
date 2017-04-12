-- Généré le :  Dim 12 Février 2017 à 00:40
-- Version du serveur :  5.7.14
-- Version de PHP :  5.6.25

--------------------------------------------------------

--
-- Contenu de la table `amelioration`
--

INSERT INTO `amelioration` (`amelioration_id`, `amelioration_nom`, `amelioration_pv`, `amelioration_vitesse`, `amelioration_degat`, `amelioration_image`) VALUES
(1, 'bouclier anti-missile', 50, 0, 0, NULL),
(2, 'Refroidisseur à hydrogène nucléaire', 0, 5, 10, NULL),
(3, 'Propulseur cosmique', 0, 15, 0, NULL),
(4, 'Chargeur rapide', 0, 0, 10, NULL);

--
-- Contenu de la table `arme`
--

INSERT INTO `arme` (`arme_id`, `arme_nom`, `arme_type`, `arme_degat`, `arme_munition`, `arme_recharge`, `arme_image`) VALUES
(1, 'Canon laser', 'Canon', 15, 5, 5, NULL),
(2, 'Tourelle plasma', 'Mitraillette', 7, 20, 5, NULL),
(3, 'Quadri-Turbolaser', 'Canon', 25, 4, 15, NULL),
(4, 'Bombe à photon', 'Bombe', 30, 1, 10, NULL);

--
-- Contenu de la table `joueur`
--

INSERT INTO `joueur` (`joueur_id`, `joueur_nom`, `joueur_mdp`, `joueur_niveau`, `joueur_xp`) VALUES
(1, 'lucas', '1a1dc91c907325c69271ddf0c944bc72', 5, 500),
(2, 'theo', '1a1dc91c907325c69271ddf0c944bc72', 4, 40),
(3, 'nathan', '1a1dc91c907325c69271ddf0c944bc72', 6, 600),
(4, 'elisa', '1a1dc91c907325c69271ddf0c944bc72', 1, 0);

--
-- Contenu de la table `materiau`
--

INSERT INTO `materiau` (`materiau_id`, `materiau_nom`, `materiau_description`, `materiau_rarete`, `materiau_image`) VALUES
(1, 'Plasma', NULL, 'Commun', NULL),
(2, 'Carbone', NULL, 'Commun', NULL),
(3, 'Hydrogène cosmique', NULL, 'Commun', NULL),
(4, 'Photon', NULL, 'Commun', NULL),
(5, 'Redstone', NULL, 'Commun', NULL);


--
-- Contenu de la table `vaisseau`
--

INSERT INTO `vaisseau` (`vaisseau_id`, `vaisseau_nom`, `vaisseau_type`, `vaisseau_pv`, `vaisseau_degat`, `vaisseau_vitesse`, `vaisseau_image`) VALUES
(1, 'X-Wing', 'Chasseur', 450, 0, 50, NULL),
(2, 'Nef Royale', 'Bombardier', 700, 0, 20, NULL);

--
-- Contenu de la table `inventaire`
--

INSERT INTO `inventaire` (`inventaire_joueur_id`, `inventaire_materiau_id`, `inventaire_quantite`) VALUES
(1, 1, 2),
(1, 2, 3),
(1, 3, 7),
(1, 5, 5),
(2, 1, 4),
(2, 2, 3),
(2, 4, 8),
(3, 2, 5),
(3, 4, 6),
(3, 5, 1);

--
-- Contenu de la table `joueur_vaisseau_amelioration`
--

INSERT INTO `joueur_vaisseau_amelioration` (`jva_joueur_id`, `jva_vaisseau_id`, `jva_amelioration_id`) VALUES
(1, 1, 1),
(2, 1, 1),
(1, 1, 2),
(3, 1, 2),
(2, 2, 3),
(1, 2, 4),
(2, 2, 4),
(3, 1, 4);

--
-- Contenu de la table `materiau_amelioration`
--

INSERT INTO `materiau_amelioration` (`ma_materiau_id`, `ma_amelioration_id`, `ma_quantite`) VALUES
(1, 1, 2),
(1, 2, 2),
(1, 3, 4),
(2, 1, 5),
(3, 2, 10),
(3, 3, 5),
(5, 3, 1),
(5, 4, 3);


--
-- Contenu de la table `vaisseau_arme`
--

INSERT INTO `vaisseau_arme` (`va_vaisseau_id`, `va_arme_id`) VALUES
(1, 1),
(1, 2),
(2, 3),
(1, 4),
(2, 4);
