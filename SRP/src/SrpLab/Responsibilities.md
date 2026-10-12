# Responsibilities.md

## 1. AppointmentDesk

- **Appointment scheduling:** Finding available slots, booking appointments, and tracking booked slots.
- **Business hours policy:** Determining whether an appointment falls within working hours.
- **Calendar formatting:** Generating ICS calendar content.
- **Reminder formatting:** Creating SMS appointment reminders.

## 2. CheckoutBasket

- **Basket management:** Adding items and storing basket data.
- **Pricing calculations:** Calculating subtotal, discounts, and grand total.
- **Coupon policy:** Validating coupon text and calculating discounts.
- **Gift wrapping:** Applying gift-wrap fees.
- **Gift message formatting:** Generating gift message cards.
- **Payment authorization:** Generating payment authorization stub codes.

## 3. CourseEnrollmentDesk

- **Enrollment management:** Registering students, preventing duplicate registrations, managing capacity, and maintaining the waitlist.
- **Waitlist management:** Calculating waitlist positions and promoting students.
- **Welcome packet formatting:** Generating student welcome information in Markdown.
- **Tuition invoice formatting:** Producing tuition and VAT invoice lines.

## 4. GradeBook

- **Grade recording:** Storing student scores and validating score ranges.
- **Grade aggregation:** Calculating student averages.
- **Letter-grade policy:** Mapping averages to letter grades.
- **Honor-roll policy:** Determining whether students qualify for the honor roll.
- **Transcript formatting:** Generating plain-text transcripts.
- **CSV export:** Exporting student grades and honor-roll information.

## 5. KitchenTicket

- **Kitchen order management:** Storing ordered items, ingredients, and preparation times.
- **Allergen detection:** Identifying allergens based on ingredient names.
- **Preparation-time estimation:** Calculating estimated ready times based on preparation durations and available stations.
- **Thermal ticket formatting:** Generating printable kitchen tickets.
- **Expo lane policy:** Choosing the appropriate lane based on allergens and estimated preparation time.

## 6. LoanDesk

- **Loan risk calculation:** Calculating the applicant's risk score.
- **Eligibility policy:** Determining whether the applicant meets approval requirements.
- **Document policy:** Determining the required loan documents.
- **Decision letter formatting:** Generating loan approval or rejection letters.
- **CSV export:** Formatting loan application data for underwriting reports.

## 7. WarehousePickList

- **Pick-list management:** Storing required items and available stock quantities.
- **Stock allocation:** Calculating quantities that can be allocated from available inventory.
- **Walking-order calculation:** Sorting picking tasks by aisle and bin.
- **Picker instruction formatting:** Generating picking instructions and shortage warnings.
- **WMS XML formatting:** Generating XML batches for warehouse management system integration.

## 8. WardBoard

- **Bed and patient tracking:** Assigning patients to beds and storing their acuity scores.
- **Acuity scoring:** Calculating patient acuity from heart rate and oxygen saturation.
- **Alert policy:** Determining when to trigger a pager alert and which handoff tone to use.
- **Pager log management:** Recording and draining pager alerts.
- **Handoff note formatting:** Generating nurse handoff notes.
- **Census CSV export:** Exporting bed, patient, and acuity information.

## 9. SupportTicket

- **Ticket management:** Storing ticket details and appending customer messages.
- **Priority classification:** Determining ticket priority from subject and body keywords.
- **SLA policy:** Calculating deadlines and determining whether an SLA has been breached.
- **Public reply formatting:** Generating customer-facing support replies.
- **Internal escalation formatting:** Generating internal escalation summaries.

## 10. SubscriptionBilling

- **Subscription data management:** Storing customer, pricing, billing-period, and failed-payment information.
- **Proration calculation:** Calculating charges based on the active subscription period.
- **Invoice number generation:** Generating invoice identifiers.
- **Failed-payment tracking:** Recording