
(ns main
  (:require [ring.adapter.jetty :as jetty]
            [clojure.string :as str]))

(def students
  [{:name "Subhrajeet" :marks 88}
   {:name "Deep" :marks 75}
   {:name "Harsh" :marks 45}])

(def passed-students
  (filter (fn [s] (>= (:marks s) 50)) students))

(def student-summary
  (map (fn [s]
         (str (:name s) " passed with " (:marks s) " marks"))
       passed-students))

(def total-marks
  (reduce + (map :marks passed-students)))

(def db-url "jdbc:sqlite:students.db")

(defn init-db []
  (try
    (with-open [conn (java.sql.DriverManager/getConnection db-url)
                stmt (.createStatement conn)]
      (.executeUpdate stmt "DROP TABLE IF EXISTS students")
      (.executeUpdate stmt "CREATE TABLE students (name TEXT, marks INT)")
      (doseq [s students]
        (.executeUpdate stmt
                        (format "INSERT INTO students VALUES ('%s', %d)"
                                (:name s) (:marks s))))
      (println "Database created successfully!"))
    (catch Exception e
      (println "Database Error:" (.getMessage e)))))

(defn read-db []
  (try
    (with-open [conn (java.sql.DriverManager/getConnection db-url)
                stmt (.createStatement conn)
                rs (.executeQuery stmt "SELECT * FROM students")]
      (loop [records []]
        (if (.next rs)
          (recur (conj records
                       (str (.getString rs 1) ": "
                            (.getInt rs 2))))
          records)))
    (catch Exception e
      (println "Read Error:" (.getMessage e))
      [])))

(defn handler [request]
  {:status 200
   :headers {"Content-Type" "text/plain"}
   :body (str
          "=== Student Portal ===\n\n"
          "Passed Students:\n"
          (str/join "\n" student-summary)
          "\n\nTotal Marks: " total-marks
          "\n\nDatabase Records:\n"
          (str/join "\n" (read-db)))})

(defn -main []
  (println "=== Student Results ===")
  (doseq [s student-summary]
    (println s))

  (println "Total Marks:" total-marks)

  (println "\n=== SQLite Database ===")
  (init-db)

  (println "Database Records:")
  (doseq [record (read-db)]
    (println record))

  (println "\n=== HTTP Server ===")
  (try
    (jetty/run-jetty handler {:port 3000 :join? false})
    (println "Server running at http://localhost:3000")
    (catch Exception _
      (jetty/run-jetty handler {:port 4000 :join? false})
      (println "Server running at http://localhost:4000"))))
