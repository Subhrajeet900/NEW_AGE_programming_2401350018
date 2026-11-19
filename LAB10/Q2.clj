(println "=== COLLECTION 1: NUMBERS ===")
(def scores [10 20 25 30 45 50 65 70 80 90])
(println "Original numbers:" scores)

(println "\n1. map (square each number):")
(def squared (map (fn [x] (* x x)) scores))
(println squared)

(println "\n2. filter (only even numbers):")
(def evens (filter even? scores))
(println evens)

(println "\n3. reduce (sum and maximum):")
(def total-sum (reduce + scores))
(def max-score (reduce max scores))
(println "Sum:" total-sum)
(println "Max:" max-score)

(println "\n4. take and drop:")
(def first-three (take 3 scores))
(def after-three (drop 3 scores))
(println "take 3:" first-three)
(println "drop 3:" after-three)

(println "\n5. Threading macro (->>):")
(def chained-result
  (->> scores
       (filter even?)
       (take 3)))
(println "Even numbers (first 3):" chained-result)


(println "\n=== COLLECTION 2: STUDENT RECORDS ===")
(def students [
  {:name "Subhrajeet" :roll "2401350018" :marks 85}
  {:name "Deep"       :roll "2401350019" :marks 72}
  {:name "Harsh"      :roll "2401350020" :marks 45}
  {:name "Aman"       :roll "2401350021" :marks 90}
  {:name "Rohan"      :roll "2401350022" :marks 60}
])
(println "Original student records:")
(doseq [s students]
  (println s))

(println "\n1. map (extract student names):")
(def student-names (map :name students))
(println student-names)

(println "\n2. filter (students with marks >= 60):")
(def passed-students (filter (fn [s] (>= (:marks s) 60)) students))
(doseq [s passed-students]
  (println s))

(println "\n3. reduce (total marks of all students):")
(def total-student-marks
  (reduce + (map :marks students)))
(println "Total Marks:" total-student-marks)

(println "\n4. take and drop (top 2 students, skip first 2):")
(def top-two (take 2 students))
(def remaining (drop 2 students))
(println "take 2:")
(doseq [s top-two]
  (println s))
(println "drop 2:")
(doseq [s remaining]
  (println s))

(println "\n5. Threading macro (->>):")
(def chained-students
  (->> students
       (filter (fn [s] (>= (:marks s) 70)))
       (map :name)))
(println "High scorers (marks >= 70):" chained-students)

