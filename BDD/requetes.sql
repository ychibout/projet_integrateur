-- Selectionner un joueur
SELECT * FROM joueur WHERE joueur_nom = 'lucas';
SELECT * FROM joueur WHERE joueur_nom = 'lucas' AND joueur_mdp = '1a1dc91c907325c69271ddf0c944bc72';

-- Insérer un nouveau joueur
-- INSERT INTO joueur (joueur_nom, joueur_mdp) VALUES ('yanis', '1a1dc91c907325c69271ddf0c944bc72');


-- Selectionner les améliorations montées sur les vaisseaux d'un joueur
SELECT amelioration_nom, amelioration_pv, amelioration_degat, amelioration_vitesse, amelioration_id, vaisseau_nom, vaisseau_id, joueur_nom, joueur_id
FROM joueur_vaisseau_amelioration
INNER JOIN amelioration ON jva_amelioration_id = amelioration_id
INNER JOIN vaisseau ON jva_vaisseau_id = vaisseau_id
INNER JOIN joueur ON jva_joueur_id = joueur_id
WHERE joueur_id = 1;


-- Selectionner les caracteristiques d'un vaisseau d'un joueur avec une certaine arme
SELECT joueur_nom, vaisseau_id, vaisseau_nom,
  (vaisseau_pv + SUM(amelioration_pv)),
  (SUM(amelioration_degat) +  arme_degat + vaisseau_degat),
  (vaisseau_vitesse + SUM(amelioration_vitesse))
FROM vaisseau_arme
INNER JOIN vaisseau ON va_vaisseau_id = vaisseau_id
INNER JOIN joueur_vaisseau_amelioration ON vaisseau_id = jva_vaisseau_id
INNER JOIN amelioration ON jva_amelioration_id = amelioration_id
INNER JOIN joueur ON jva_joueur_id = joueur_id
INNER JOIN arme ON va_arme_id = arme_id
WHERE vaisseau_id = 1 AND arme_id = 2 AND joueur_id = 1
GROUP BY vaisseau_id;


-- Selectionner les caractéristiques des améliorations montées sur les vaisseaux d'un joueur
SELECT (vaisseau_pv + SUM(amelioration_pv)), (vaisseau_degat + SUM(amelioration_degat)), (vaisseau_vitesse + SUM(amelioration_vitesse)), vaisseau_nom, joueur_nom
FROM joueur_vaisseau_amelioration
INNER JOIN vaisseau on vaisseau_id = jva_vaisseau_id
INNER JOIN joueur on joueur_id = jva_joueur_id
INNER JOIN amelioration on amelioration_id = jva_amelioration_id
WHERE joueur_id = 1
GROUP BY vaisseau_pv, vaisseau_degat, vaisseau_vitesse, vaisseau_nom, joueur_nom;


-- Selectionner la liste des armes d'un vaisseau
SELECT arme_id, arme_nom, arme_degat, arme_munition, arme_recharge, vaisseau_nom, vaisseau_id
FROM vaisseau_arme
INNER JOIN vaisseau on vaisseau_id = va_vaisseau_id
INNER JOIN arme on arme_id = va_arme_id
WHERE vaisseau_id = 1
ORDER BY vaisseau_id;


-- Selectionner la liste des materiaux dans l'inventaire d'un joueur
SELECT materiau_nom, inventaire_quantite
FROM inventaire
INNER JOIN materiau on inventaire_materiau_id = materiau_id
INNER JOIN joueur ON inventaire_joueur_id = joueur_id
WHERe joueur_id = 1;


-- Selectionner la liste des materiaux nécessaire pour une amélioration
SELECT amelioration_id, amelioration_nom, materiau_id, materiau_nom, ma_quantite
FROM materiau_amelioration
INNER JOIN materiau ON ma_materiau_id = materiau_id
INNER JOIN  amelioration ON ma_amelioration_id = amelioration_id
ORDER BY amelioration_id, materiau_id;
