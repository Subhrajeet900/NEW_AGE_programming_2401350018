(defn validate-student [student]
  (if (< (:marks student) 0)
    (throw (ex-info "Invalid marks: Marks cannot be negative"
                    {:student (:name student)
                     :roll (:roll student)
                     :error-code :negative-marks}))
    student))

(defn test-arithmetic [a b]
  (try
    (println "Attempting to divide" a "by" b)
    (println "Result:" (/ a b))
    (catch ArithmeticException e
      (println "Caught ArithmeticException:" (.getMessage e)))
    (finally
      (println "Finally: Arithmetic cleanup completed."))))

(defn test-null-pointer [text]
  (try
    (println "\nAttempting to get length of string:")
    (println "Length:" (.length text))
    (catch NullPointerException e
      (println "Caught NullPointerException: Text is null, cannot call .length"))
    (finally
      (println "Finally: Null pointer check completed."))))

(defn test-custom-exception [student]
  (try
    (println "\nAttempting to validate student:" (:name student))
    (validate-student student)
    (println "Validation successful!")
    (catch clojure.lang.ExceptionInfo e
      (println "Caught Custom Exception (ExceptionInfo)!")
      (println "ex-message:" (ex-message e))
      (println "ex-data:" (ex-data e))
      (println "Graceful Recovery: Setting default marks 0 for" (:student (ex-data e))))
    (finally
      (println "Finally: Student validation completed."))))

(println "=== 1. Handling ArithmeticException ===")
(test-arithmetic 100 0)

(println "\n=== 2. Handling NullPointerException ===")
(test-null-pointer nil)

(println "\n=== 3. Custom Exception with ex-info, ex-message, and ex-data ===")
(def invalid-student {:name "Subhrajeet" :roll "2401350018" :marks -15})
(test-custom-exception invalid-student)

(def valid-student {:name "Subhrajeet" :roll "2401350018" :marks 95})
(test-custom-exception valid-student)

